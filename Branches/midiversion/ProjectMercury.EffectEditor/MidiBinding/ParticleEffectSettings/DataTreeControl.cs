using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ProjectMercury;
using ProjectMercury.EffectEditor.TreeNodes;

namespace BindingLibrary
{
    public partial class DataTreeControl : UserControl
    {
        private bool blockEvents;
        private IBindableObject selectedObject;
        private string selectedTreeProperty;
        private string selectedGridProperty;

        public event EventHandler SelectionChanged;


        public DataTreeControl()
        {
            InitializeComponent();
        }

        public void Initialize(ParticleEffect particleEffect)
        {

            try
            {
                blockEvents = true;


                //TreeNode node = BuildTree();


                var node = new ParticleEffectTreeNode(particleEffect);
                treeView1.Nodes.Add(node);

                treeView1.SelectedNode = node;

                treeView1.SelectedNode.ExpandAll();
            }

            finally
            {
                blockEvents = false;
            }

        }


        private TreeNode BuildTree()
        {

            TreeNode rootNode = new TreeNode("Xna Engine");

            TreeNode propertiesNode = new TreeNode("properties");
           // propertiesNode.Tag = DataFactory.Instance.LayerProperties;


            TreeNode rendererNode = new TreeNode("TriangleRenderer");
          //  rendererNode.Tag = DataFactory.Instance.LayerProperties.TriangleRenderer;
            rootNode.Nodes.Add(propertiesNode);

            propertiesNode.Nodes.Add(rendererNode);
            

            return rootNode;

        }


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string propName = "";

            // first object in the graph that is IBindable
            selectedObject = GetObjectAndPropertyNameFromTree(e.Node, ref propName);
            // path to IBindable object
            selectedTreeProperty = propName;


            // init property Grid.
            propertyGrid1.SelectedObject = e.Node.Tag;

          
        

            if (SelectionChanged != null) SelectionChanged(this, EventArgs.Empty);

        }




        private void propertyGrid1_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {

            selectedGridProperty = GetGridPropertyName(e.NewSelection);

            if (SelectionChanged != null) SelectionChanged(this, EventArgs.Empty);

        }

        private IBindableObject GetObjectAndPropertyNameFromTree(TreeNode node, ref string propertyName)
        {
            if (node != null && node.Tag != null)
            {
                if (node.Tag is IBindableObject)
                {
                    return node.Tag as IBindableObject;
                }
                else
                {
                    // add the tag
                    propertyName += String.IsNullOrEmpty(propertyName) ? node.Text : ("." + node.Text);

                    return GetObjectAndPropertyNameFromTree(node.Parent, ref propertyName);
                }
            }
            return null;
        } 
        
        
        private static string GetGridPropertyName(GridItem node)
        {

            if (node != null && node.Value != null)
            {

                string parentName = GetGridPropertyName(node.Parent);

                return String.IsNullOrEmpty(parentName) ? node.PropertyDescriptor.Name : (parentName +  "." + node.PropertyDescriptor.Name);
            }
            return "";
        }

        public string SelectedTreeProperty
        {
            get {

                string path = "";
                if (String.IsNullOrEmpty(selectedTreeProperty))
                {
                    
                    if (! String.IsNullOrEmpty(selectedGridProperty))
                    {
                        path += selectedGridProperty;                        
                    }
                }
                else
                {
                    path += selectedTreeProperty;
                   
                    if (!String.IsNullOrEmpty(selectedGridProperty))
                    {
                        path += "." + selectedGridProperty;
                    }

                }

                return path;
            }
        }

        public IBindableObject SelectedObject
        {
            get {
                return selectedObject;
            }
        }


      
    }
}



/*
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                blockEvents = true;

                switch ((e.Action))
                {
                    case TreeViewAction.ByMouse:

                        if (e.Node.Tag != null)
                        {

                            string propName = "";
                            IBindableObject bindableObject = GetObjectAndPropertyName(e.Node, ref propName);

                            


                            if (bindableObject != null)
                            {

                                if (String.IsNullOrEmpty(propName)) 
                                {
                                    propertyGrid1.SelectedObject = bindableObject;
                                }
                                else
                                {
                                    string[] properties = propName.Split('.');

                                    object start = bindableObject;
                                    
                                    foreach (string s in properties)
                                    {
                                        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(bindableObject)[s];
                                        if ((start != null) && (descriptor != null))
                                        {
                                            start = descriptor.GetValue(start);
                                        }
                                    }
                                    
                                    if (start != null) propertyGrid1.SelectedObject = start;
                                }


                                GridItem item = propertyGrid1.SelectedGridItem;
                                if (item != null && item.PropertyDescriptor != null)
                                {


                                    selectedObject = bindableObject;
                                    selectedProperty = propName;

                                    if (SelectionChanged != null) SelectionChanged(this, EventArgs.Empty);

                                }
                            }
                        }

                        break;

                }
            }
            finally
            {
                blockEvents = false;
            }


        }
*/