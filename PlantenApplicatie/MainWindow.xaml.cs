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
using Microsoft.EntityFrameworkCore;
using PlantenApplicatie.DATA;

namespace PlantenApplicatie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        public void addItemsToComboBox(ComboBox plant, string item)
        {
            
        }

        public void addItemsTocbxSoort()
        {

        }

        public void addItemsTocbGeslacht()
        {

        }
        
        public void addItemsTocbxVariant()
        {

        }

        private void cbxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cbxFamilie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cbxSoort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxGeslacht_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void cbxVariant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
