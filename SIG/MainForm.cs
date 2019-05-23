using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;


using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.GISClient;

namespace SIG
{
    public sealed partial class MainForm : Form
    {
        string accion = " ";
        int c = 0;
        #region class private members
        private IMapControl3 m_mapControl = null;
        private string m_mapDocumentName = string.Empty;
        #endregion

        #region class constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.cargarComboBuscarSucursal();
            //this.cargarComboBuscarCliente();
            //get the MapControl
            //m_mapControl = (IMapControl3)axMapControl1.Object;

            ////disable the Save menu (since there is no document yet)
            //menuSaveDoc.Enabled = false;
        }

        #region Main Menu event handlers
        private void menuNewDoc_Click(object sender, EventArgs e)
        {
            //execute New Document command
            ICommand command = new CreateNewDocument();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuOpenDoc_Click(object sender, EventArgs e)
        {
            //execute Open Document command
            ICommand command = new ControlsOpenDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        //private void menuSaveDoc_Click(object sender, EventArgs e)
        //{
        //    //execute Save Document command
        //    if (m_mapControl.CheckMxFile(m_mapDocumentName))
        //    {
        //        //create a new instance of a MapDocument
        //        IMapDocument mapDoc = new MapDocumentClass();
        //        mapDoc.Open(m_mapDocumentName, string.Empty);

        //        //Make sure that the MapDocument is not readonly
        //        if (mapDoc.get_IsReadOnly(m_mapDocumentName))
        //        {
        //            MessageBox.Show("Map document is read only!");
        //            mapDoc.Close();
        //            return;
        //        }

        //        //Replace its contents with the current map
        //        mapDoc.ReplaceContents((IMxdContents)m_mapControl.Map);

        //        //save the MapDocument in order to persist it
        //        mapDoc.Save(mapDoc.UsesRelativePaths, false);

        //        //close the MapDocument
        //        mapDoc.Close();
        //    }
        //}

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            //execute SaveAs Document command
            ICommand command = new ControlsSaveAsDocCommandClass();
            command.OnCreate(m_mapControl.Object);
            command.OnClick();
        }

        private void menuExitApp_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
        #endregion

        //listen to MapReplaced evant in order to update the statusbar and the Save menu
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            //get the current document name from the MapControl
            m_mapDocumentName = m_mapControl.DocumentFilename;

            //if there is no MapDocument, diable the Save menu and clear the statusbar
            if (m_mapDocumentName == string.Empty)
            {
                //menuSaveDoc.Enabled = false;
                //statusBarXY.Text = string.Empty;
            }
            else
            {
                //enable the Save manu and write the doc name to the statusbar
                //menuSaveDoc.Enabled = true;
                //statusBarXY.Text = Path.GetFileName(m_mapDocumentName);
            }
        }

        private void btnMover_Click(object sender, EventArgs e)
        {
            accion = "mover";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            accion = "ampliar";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mapa1.Extent = Mapa1.FullExtent;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mapa1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerZoomOut;
            accion = "reducir";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Mapa1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            Mapa1.ShowPropertyPages();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Mapa1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerArrowQuestion;
            Mapa1.ClearLayers();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Mapa1.Rotation = Mapa1.Rotation + 20;
            Mapa1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Mapa1.Rotation = Mapa1.Rotation - 20;
            Mapa1.Refresh();
        }

        private void Mapa1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
         


            if (accion == "mover")
            {
                Mapa1.MousePointer = esriControlsMousePointer.esriPointerPan;
                Mapa1.Pan();
            }
            if (accion == "ampliar")
            {
                Mapa1.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
                Mapa1.Extent = Mapa1.TrackRectangle();
            }
            if (accion == "reducir")
            {
                Mapa1.MousePointer = esriControlsMousePointer.esriPointerPageZoomOut;
                switch (accion)
                {
                    case "reducir":
                        IEnvelope zoomout;
                        zoomout = Mapa1.Extent;
                        zoomout.Expand(1.5, 1.5, true);
                        Mapa1.Extent = zoomout;
                        Mapa1.Refresh();
                        break;
                }
            }

            if (accion == "identificacion")
            {
                Mapa1.MousePointer = esriControlsMousePointer.esriPointerIdentify;


            }
         }

        private void button8_Click(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (c < 72)
            {
                Mapa1.Rotation = Mapa1.Rotation - 5;
                Mapa1.Refresh();
                c++;
            }
            else
            {
                c = 0;
                this.timer1.Stop();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (this.chkCliente.Checked == true)
            {
                IMap map = Mapa1.Map;
                IFeatureSelection featSel;
                IFeatureLayer fLayer;
                fLayer = Mapa1.get_Layer(1) as IFeatureLayer;
                featSel = fLayer as IFeatureSelection;
                IQueryFilter qFilt = new QueryFilter();
                //qFilt.WhereClause = "idpersona LIKE '%" + crops  + "%'";
                qFilt.WhereClause = "OBJECTID =" + Convert.ToInt32(txtBuscar.Value);
                //qFilt.WhereClause = "idpersona LIKE % "+ crops + "%";
                //qFilt.WhereClause = "idpersona = " + crops ;
                //qFilt.WhereClause = "NOMBRE like '%" + crops + "%'";
                featSel.Clear();


                featSel.SelectFeatures(qFilt, esriSelectionResultEnum.esriSelectionResultNew, false);
                MessageBox.Show(featSel.SelectionSet.Count.ToString() + " Usuario Marcado");
                IRgbColor grn = new RgbColor();
                grn.Red = 255;
                grn.Green = 0;
                grn.Blue = 0;
                featSel.SelectionColor = grn;
                Mapa1.Refresh();
            }
            else
            {
                IMap map = Mapa1.Map;
                IFeatureSelection featSel;
                IFeatureLayer fLayer;
                fLayer = Mapa1.get_Layer(0) as IFeatureLayer;
                featSel = fLayer as IFeatureSelection;
                IQueryFilter qFilt = new QueryFilter();
                //qFilt.WhereClause = "idpersona LIKE '%" + crops  + "%'";
                qFilt.WhereClause = "OBJECTID =" + Convert.ToInt32(txtBuscar.Value);
                //qFilt.WhereClause = "idpersona LIKE % "+ crops + "%";
                //qFilt.WhereClause = "idpersona = " + crops ;
                //qFilt.WhereClause = "NOMBRE like '%" + crops + "%'";
                featSel.Clear();


                featSel.SelectFeatures(qFilt, esriSelectionResultEnum.esriSelectionResultNew, false);
                MessageBox.Show(featSel.SelectionSet.Count.ToString() + " Usuario Marcado");
                IRgbColor grn = new RgbColor();
                grn.Red = 255;
                grn.Green = 0;
                grn.Blue = 0;
                featSel.SelectionColor = grn;
                Mapa1.Refresh();
            }

           
        }

      
        private void cargarCombo()
        {
            //this.txtBuscar.DropDownList.Columns.Clear();
            //this.txtBuscar.DropDownList.Columns.Add("nombre_dpto").Width = 100;
            //this.txtBuscar.DropDownList.Columns["nombre_dpto"].Caption = "nombre_dpto";
            //this.txtBuscar.ValueMember = "id_dpto";
            //this.txtBuscar.DisplayMember = "nombre_dpto";


            //IMap map = Mapa1.Map;
            //IFeatureSelection featSel;
            //IFeatureLayer fLayer;
            //fLayer = Mapa1.get_Layer(2) as IFeatureLayer;
            //featSel = fLayer as IFeatureSelection;
            //IQueryFilter qFilt = new QueryFilter();
            ////qFilt.WhereClause = "idpersona LIKE '%" + crops  + "%'";
            //qFilt.WhereClause = "Nombre" + txtBuscar.Text;

           
        }

        private void txtBuscar1_ValueChanged(object sender, EventArgs e)
        {
            this.cargarCombo();
        }

      
        private void cargarComboBuscarSucursal()
        {

            this.txtBuscar.DropDownList.Columns.Clear();
            this.txtBuscar.DropDownList.Columns.Add("nombre").Width = 100;
            this.txtBuscar.DropDownList.Columns.Add("descripcion").Width = 200;
            this.txtBuscar.DropDownList.Columns["nombre"].Caption = "Nombre";
            this.txtBuscar.DropDownList.Columns["descripcion"].Caption = "descripcion";
            this.txtBuscar.ValueMember = "OBJECTID";
            this.txtBuscar.DisplayMember = "nombre";
            Consulta Obj = new Consulta();
            this.txtBuscar.DataSource = Obj.buscarStringSucursal(this.txtBuscar.Text);
            this.txtBuscar.Refresh();
        }
        private void txtBuscar_ValueChanged(object sender, EventArgs e)
        {
            if(this.chkCliente.Checked==true)
            {
                this.cargarComboBuscarCliente();
                Consulta Obj = new Consulta();
                this.dataListado.DataSource = Obj.buscarStringCliente(this.txtBuscar.Text);
            }
            else
            {
                if(this.chkSucursal.Checked==true)
                {
                    this.cargarComboBuscarSucursal();
                    Consulta Obj = new Consulta();
                    this.dataListado.DataSource = Obj.buscarStringSucursal(this.txtBuscar.Text);
                }
            }
           
            this.dataListado.AutoResizeRows();
            this.dataListado.AutoResizeColumns();
        }
        private void cargarComboBuscarCliente()
        {

            this.txtBuscar.DropDownList.Columns.Clear();
            this.txtBuscar.DropDownList.Columns.Add("nombre").Width = 100;
            this.txtBuscar.DropDownList.Columns.Add("apellido").Width =100;
            this.txtBuscar.DropDownList.Columns["nombre"].Caption = "Nombre";
            this.txtBuscar.DropDownList.Columns["apellido"].Caption = "apellido";
            this.txtBuscar.ValueMember = "idCliente";
            this.txtBuscar.DisplayMember = "nombre";
            Consulta Obj = new Consulta();
            this.txtBuscar.DataSource = Obj.buscarStringCliente(this.txtBuscar.Text);
            this.txtBuscar.Refresh();
        }

    }
}