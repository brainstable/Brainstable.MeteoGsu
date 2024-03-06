using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Brainstable.Filial.Location;
using Brainstable.Meteo;
using Syncfusion.Windows.Forms.Tools;
using System.Reflection;

namespace Brainstable.Filial
{
    public partial class FormMain : Form
    {
        private DalLocations dalLocations;

        private List<Plot> plots;
        private List<District> districts;
        private Plot currentPlot;
        private RegistartionJournalMeteo meteoRegistration;
        private List<MeteoStation> stations;

        private InitMap map;

        public FormMain()
        {
            dalLocations = new DalLocations();
            InitilizeData();
            

            InitializeComponent();
            Text = $"МетеоГСУ, ver. {Assembly.GetExecutingAssembly().GetName().Version.Major}.{Assembly.GetExecutingAssembly().GetName().Version.Minor}.{Assembly.GetExecutingAssembly().GetName().Version.Build}";
            
            
            InitializeTreeNavigator(); 
            meteoRegistration = RegistartionJournalMeteo.RegistartionPlots(plots, stations);


            map = new InitMap();
            

            webBrowser1.Url = map.GetUriByPlotId();
           
        }

        private FileMeteo fileMeteo;
       
        private void InitilizeData()
        { 
            plots = dalLocations.GetPlots();
            districts = FactoryDistricts.CreateDistricts(plots);

            fileMeteo = new FileMeteo();
            stations = fileMeteo.GetMeteoStations();
        }

        private void treeNavigator1_SelectionChanged(TreeNavigator sender, SelectionStateChangedEventArgs e)
        {
            TreeMenuItem tm = e.SelectedItem;

            statusStripLabel1.Text = tm.Text;

            if (e.SelectedItem.Tag != null)
            {
                if (e.SelectedItem.Tag.GetType() == typeof(Plot))
                {
                    currentPlot = e.SelectedItem.Tag as Plot;
                    if (meteoRegistration.RegistrationNotes.ContainsKey(currentPlot.Id))
                    {
                        LoadMeteo(meteoRegistration.RegistrationNotes[currentPlot.Id]);
                        webBrowser1.Url = map.GetUriByPlotId(currentPlot.Id);
                    }
                    else
                    {
                        MessageBox.Show("На данном сортоучастке не зарегистрирована ни одна метеостанция.",
                            "Внимание!",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

       

       

        private void LoadMeteo(MeteoPlot meteoPlot)
        {
            //try
            {
                Cursor = Cursors.WaitCursor;
                tabPage1.Controls.Clear();
                WeatherViewer viewer = new WeatherViewer(meteoPlot.MeteoStation, meteoPlot.Plot.Name, 
                    meteoPlot.Plot.AblativeName, meteoPlot.Distance);
                viewer.Dock = DockStyle.Fill;
                tabPage1.Controls.Add(viewer);
                Cursor = Cursors.Default;
            }
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}
        }

        private void InitializeTreeNavigator()
        {
            List<TreeMenuItem> treeMenuItems = new List<TreeMenuItem>();
            foreach (District district in districts)
            {
                TreeMenuItem tmi = new TreeMenuItem();
                tmi.Text = district.Name.ToUpper();
                tmi.Tag = district;
                treeNavigator1.Items.Add(tmi);
                treeMenuItems.Add(tmi);

                foreach (Plot plot in district.Plots.OrderBy(p => p.Name))
                {
                    TreeMenuItem tmich = new TreeMenuItem();
                    tmich.Text = plot.Name;
                    tmich.Tag = plot;
                    tmi.Items.Add(tmich);
                }
            }
            this.treeNavigator1.SelectionChanged += new SelectionStateChangedEventHandler(treeNavigator1_SelectionChanged);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout about = new FormAbout();
            about.ShowDialog();
        }

        private void updateMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Url = map.Uri;
            }
            catch { }
        }
    }
}
