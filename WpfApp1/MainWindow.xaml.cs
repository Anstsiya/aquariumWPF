using aquarium.aquarium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<Predator> predators = new List<Predator>() { new Predator(new int[] { 2, 3 }, "Timofey", 5, true),
           new Predator(new int[] { 1, 4 }, "Pepa", 7, false, 5, true, 3 ) } ;
        static List<Herbivore> herbivores = new List<Herbivore>() { new Herbivore(new int[] { 0, 2 }, "Herbik", 2, false) };
        static List<Rock> rocks = new List<Rock>() { new Rock(new int[] { 3, 1 }, "Black Stone") };
        static List<Seaweed> seaweeds = new List<Seaweed>() { new Seaweed(new int[] { 0, 3 }, "Laminaris", 3) };
        Grid DynamicGrid = new Grid();
        MainWindow mainWindow ;
        Button fill = new Button();
        Button check = new Button();
        Button live = new Button();


        Aquarium aquarium = new Aquarium(6, 5, predators, herbivores, rocks, seaweeds);
        public MainWindow()
        {
            
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Top;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            RowDefinition[] rows = new RowDefinition[aquarium.aquariumSizeRow];
            ColumnDefinition[] columns = new ColumnDefinition[aquarium.aquariumSizeColumn + 1];
            for (var i = 0; i < aquarium.aquariumSizeRow; i++)
            {
                rows[i] = new RowDefinition();
                DynamicGrid.RowDefinitions.Add(rows[i]);

                rows[i].Height = new GridLength(100);


            }

            for (var j = 0; j < aquarium.aquariumSizeColumn + 1; j++)
            {
                columns[j] = new ColumnDefinition();
                DynamicGrid.ColumnDefinitions.Add(columns[j]);
                if ( j == aquarium.aquariumSizeColumn)
                {
                    columns[j].Width = new GridLength(200);
                } else
                {
                    columns[j].Width = new GridLength(100);
                }
            }
            

            fill.Content = "Fill";
            fill.Height = 25;
            fill.Width = 100;
            Grid.SetRow(fill, 0);
            Grid.SetColumn(fill, aquarium.aquariumSizeColumn + 1);
            fill.Click += new RoutedEventHandler(Button_Click_1);
            DynamicGrid.Children.Add(fill);

            mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.Content = DynamicGrid;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Label l = new Label();
            for (int i = 0; i < aquarium.aquariumSizeRow; i++)
            {
                for (int j = 0; j < aquarium.aquariumSizeColumn; j++)
                {
                    var obj = aquarium.cells[i, j];
                    if (obj != null)
                    {
                        
                        var type = obj.GetType();
                        if (type == typeof(Predator))
                        {
                            var content = (Predator)obj;
                            l = content.gridElem;
                        }
                        else if (type == typeof(Herbivore))
                        {
                            var content = (Herbivore)obj;
                            l = content.gridElem;
                        }
                        else if (type == typeof(Seaweed))
                        {
                            var content = (Seaweed)obj;
                            l = content.gridElem;
                        }
                        else if (type == typeof(Rock))
                        {
                            var content = (Rock)obj;
                            l = content.gridElem;
                        }

                        Grid.SetRow(l, i);
                        Grid.SetColumn(l, j);
                        DynamicGrid.Children.Add(l);
                    }

                }
            }

            DynamicGrid.Children.Remove(fill);

            DynamicGrid.Children.Remove(live);
            live.Content = "Live";
            live.Height = 25;
            live.Width = 100;
            Grid.SetRow(live, 2);
            Grid.SetColumn(live, aquarium.aquariumSizeColumn + 1);
            live.Click += new RoutedEventHandler(Button_Click_2);
            DynamicGrid.Children.Add(live);

            DynamicGrid.Children.Remove(check);
            check.Content = "Next Iteration";
            check.Height = 25;
            check.Width = 100;
            Grid.SetRow(check, 4);
            Grid.SetColumn(check, aquarium.aquariumSizeColumn + 1);
            check.Click += new RoutedEventHandler(Button_Click);
            DynamicGrid.Children.Add(check);

            mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.Content = DynamicGrid;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < aquarium.aquariumSizeRow; i++)
            {
                for (int j = 0; j < aquarium.aquariumSizeColumn; j++)
                {
                    var obj = aquarium.cells[i, j];
                    if (obj != null)
                    {
                        var type = obj.GetType();
                        if (type == typeof(Predator))
                        {
                            Predator currentObj = (Predator)obj;
                            currentObj.isChecked = false;
                        }
                        else if (type == typeof(Herbivore))
                        {
                            Herbivore currentObj = (Herbivore)obj;
                            currentObj.isChecked = false;
                        }
                    }
                }
            }

            for (int i = 0; i < aquarium.aquariumSizeRow; i++)
            {
                for (int j = 0; j < aquarium.aquariumSizeColumn; j++)
                {
                    var obj = aquarium.cells[i, j];
                    if (obj != null)
                    {
                        var type = obj.GetType();
                        if (type == typeof(Predator))
                        {
                            Predator currentObj = (Predator)obj;
                            currentObj.checkFish(i, j, aquarium.cells, aquarium, DynamicGrid);
                        }
                        else if (type == typeof(Herbivore))
                        {
                            Herbivore currentObj = (Herbivore)obj;
                            currentObj.checkFish(i, j, aquarium.cells, aquarium, DynamicGrid);
                        }
                        else if (type == typeof(Seaweed))
                        {
                            Seaweed currentObj = (Seaweed)obj;
                            currentObj.checkStatus();
                        }
                        else if (type == typeof(Rock))
                        {
                            Console.WriteLine("Rock");
                        }
                    }
                }
            }

            mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.Content = DynamicGrid;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < aquarium.aquariumSizeRow; i++)
            {
                for (int j = 0; j < aquarium.aquariumSizeColumn; j++)
                {
                    var obj = aquarium.cells[i, j];
                    if (obj != null)
                    {
                        var type = obj.GetType();
                        if (type == typeof(Predator))
                        {
                            Predator currentObj = (Predator)obj;
                            currentObj.isMoved = false;
                        }
                        else if (type == typeof(Herbivore))
                        {
                            Herbivore currentObj = (Herbivore)obj;
                            currentObj.isMoved = false;
                        }
                    }
                }
            }

            for (int i = 0; i < aquarium.aquariumSizeRow; i++)
            {
                for (int j = 0; j < aquarium.aquariumSizeColumn; j++)
                {
                    object obj = aquarium.cells[i, j];
                    if (obj != null)
                    {
                        var type = obj.GetType();
                        if (type == typeof(Predator))
                        {
                            Predator currentObj = (Predator)obj;
                            currentObj.lifeCycle(i, j, aquarium.cells, aquarium, DynamicGrid);
                        }
                        else if (type == typeof(Herbivore))
                        {
                            Herbivore currentObj = (Herbivore)obj;
                            currentObj.lifeCycle(i, j, aquarium.cells, aquarium, DynamicGrid);
                        }
                        else if (type == typeof(Seaweed))
                        {
                            Console.WriteLine("Seaweed");
                        }
                        else if (type == typeof(Rock))
                        {
                            Console.WriteLine("Rock");
                        }


                    }
                }
            }
            mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.Content = DynamicGrid;
        }
    }



}

