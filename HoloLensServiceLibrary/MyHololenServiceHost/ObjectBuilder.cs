//using HololensServiceHost.Properties;
//using System.Linq;
//using LMSTestLabAutomation;
//using System.Collections.Generic;
//using System;
//using System.Numerics;
//using System.Windows.Media.Media3D;

//namespace HololensServiceHost
//{
//    public partial class ObjectBuilder
//    {
//        private static ObjectBuilder _DefaultInstance;

//        private IDatabase Database = null;
//        private IGeometry DataOfGeoemetry = null;
//        private string[] ComponentNames = null;
//        private Dictionary<string, string> DictionaryOfGeoemtryNodes = new Dictionary<string, string>();

//        private Dictionary<string, string[]> components_temp = new Dictionary<string, string[]>();
        
//        private Nodes AllTheNodes = new Nodes();

//        private Array node_x_array = null;
//        private Array node_y_array = null;
//        private Array node_z_array = null;
//        private Array node_xy = null;
//        private Array node_xz = null;
//        private Array node_yz = null;

//        double[] line_start_class = null;
//        double[] line_end_class = null;

//        private string[] separators = { ";", ":" };

//        private string[] ObjectFilePart1 = null;
//        private string[] ObjectFilePart2 = null;
//        private string[] ObjectFilePart3 = null;
//        private string[] ObjectFilePart4 = null;
//        public string[] ObjectFile = null;

//        public static ObjectBuilder DefaultInstance
//        {
//            get
//            {
//                if (_DefaultInstance == null)
//                    _DefaultInstance = new ObjectBuilder();

//                return _DefaultInstance;
//            }
//        }

//        private ObjectBuilder()
//        {
//            ConfigureTestLab();
//        }
        
//        private void ConfigureTestLab()
//        {
//            Application TestlLabEnvironment = new Application();
//            if (string.IsNullOrEmpty(TestlLabEnvironment.Name))
//            {
//                TestlLabEnvironment.Init(string.Format(Settings.Default.TesTLabFilePath));
//            }
//            Database = TestlLabEnvironment.ActiveBook.Database();
//            TestlLabEnvironment.ActiveBook.SheetOnTop = Settings.Default.OnTopSheetForGeometry;
//            DataOfGeoemetry = (IGeometry)Database.GetItem(Settings.Default.GeometryDataName);
//            ComponentNames = new string[DataOfGeoemetry.ComponentNames.Length + 1];
//            DataOfGeoemetry.ComponentNames.CopyTo(ComponentNames, 0);
//            ComponentNames[ComponentNames.Length - 1] = Settings.Default.NotSpecifiedComponentName;
//        }
            
//        public Dictionary<string, string[]> GenerateObjectDictionary()
//        {
//            Dictionary<string, string[]> components = new Dictionary<string, string[]>();      
//            for (int i = 0; i < ComponentNames.Length; i++)
//            {
//                ControlFuntionOfAComponent(ComponentNames[i], out components);
//            }
//            components_temp = new Dictionary<string, string[]>();
//            DictionaryOfGeoemtryNodes.Clear();
//            return components;
//        }

//        public Dictionary<string, string[]> GenerateObjectDictionary(string componentName)
//        {
//            Dictionary<string, string[]> component = new Dictionary<string, string[]>();
//            ControlFuntionOfAComponent(componentName, out component);
//            components_temp = new Dictionary<string, string[]>();
//            DictionaryOfGeoemtryNodes.Clear();
//            return component;
//        }

//        public string[] GetComponentNames()
//        {
//            return ComponentNames;
//        }

//        private void ControlFuntionOfAComponent(string componentName, out Dictionary<string, string[]> components)
//        {

//            string[] nodename_string = null;
//            int NumberOfNode = 0;
//            string ProjectName = null;

//            double[] node_x = null;
//            double[] node_y = null;
//            double[] node_z = null;
//            string[] line_start_string = null;
//            string[] line_end_string = null;
//            string[] Trias_start = null;
//            string[] Trias_middle = null;
//            string[] Trias_end = null;
//            string[] quads_1 = null;
//            string[] quads_2 = null;
//            string[] quads_3 = null;
//            string[] quads_4 = null;

//            ChooseComponent(out NumberOfNode, out nodename_string, componentName);
//            GeneratingProjectName(out ProjectName);
//            getDataFromTesTLab(nodename_string, DataOfGeoemetry, componentName, out node_x, out node_y, out node_z, out line_start_string, out line_end_string, out Trias_start, out Trias_middle, out Trias_end, out quads_1, out quads_2, out quads_3, out quads_4);
//            buildDictionary(nodename_string, componentName, out DictionaryOfGeoemtryNodes);

//            int line_number = 0;
//            int triacs_number = 0;
//            int quads_number = 0;
//            line_number = line_start_string.Length;
//            triacs_number = Trias_start.Length;
//            quads_number = quads_1.Length;
//            double[] line_start = new double[line_number];
//            double[] line_end = new double[line_number];

//            double[] trias_start_double = new double[triacs_number];
//            double[] trias_middle_double = new double[triacs_number];

//            double[] trias_end_double = new double[triacs_number];
//            double[] quads_1_double = new double[quads_number];
//            double[] quads_2_double = new double[quads_number];
//            double[] quads_3_double = new double[quads_number];
//            double[] quads_4_double = new double[quads_number];

//            reindexing_lines(DictionaryOfGeoemtryNodes, componentName, line_number, line_start_string, line_end_string, nodename_string, out line_start, out line_end);
//            reindexing_trias(DictionaryOfGeoemtryNodes, componentName, triacs_number, Trias_start, Trias_middle, Trias_end, nodename_string, out trias_start_double, out trias_middle_double, out trias_end_double);
//            reindexing_quads(DictionaryOfGeoemtryNodes, componentName,  quads_number, quads_1, quads_2, quads_3, quads_4, nodename_string, out quads_1_double, out quads_2_double, out quads_3_double, out quads_4_double);
//            line_start_class = line_start;
//            line_end_class = line_end;
//            wireframe_nodes_string(triacs_number, quads_number, line_number, NumberOfNode, node_x, node_y, node_z);
//            wireframe_lines_string(triacs_number, quads_number, line_number, NumberOfNode, line_start, line_end);

//            string folderName = @"c:\temp\root\";
//            string project_withline = folderName + "\\wire.obj";
//            System.IO.File.WriteAllLines(project_withline, ObjectFile);
//            components_temp.Add(componentName+"_"+"wire", ObjectFile);

//            for (int i = 8 * (line_number) + NumberOfNode + 1; i <= 8 * (line_number) + NumberOfNode + triacs_number + quads_number; i++)
//            {
//                if (i > 8 * (line_number) + NumberOfNode + triacs_number)
//                {
//                    ObjectFile[i] = ("f" + " " + (1 + quads_1_double[i - 8 * (line_number) - NumberOfNode - triacs_number - 1]).ToString() + " " + (1 + quads_2_double[i - 8 * (line_number) - NumberOfNode - triacs_number - 1]).ToString() + " " + (1 + quads_3_double[i - 8 * (line_number) - NumberOfNode - triacs_number - 1]).ToString() + " " + (1 + quads_4_double[i - 8 * (line_number) - NumberOfNode - triacs_number - 1]).ToString());
//                }
//                else
//                {
//                    ObjectFile[i] = ("f" + " " + (1 + trias_start_double[i - 8 * (line_number) - NumberOfNode - 1]).ToString() + " " + (1 + trias_middle_double[i - 8 * (line_number) - NumberOfNode - 1]).ToString() + " " + (1 + trias_end_double[i - 8 * (line_number) - NumberOfNode - 1]).ToString());
//                }
//            }
//            string project_withfaces = folderName + "\\" + ProjectName + "_" + componentName + ".obj";
//            System.IO.File.WriteAllLines(project_withfaces, ObjectFile);
//            components_temp.Add(componentName, ObjectFile);

//            components = components_temp;

//            DictionaryOfGeoemtryNodes.Clear();
//        }

//        private void buildDictionary(string[] nodename_string, string componentName, out Dictionary<string, string> dictionaryOfGeoemtryNodes)
//        {
//            Dictionary<string, string> geometry_dictionary_temp = new Dictionary<string, string>();
//            for (int j = 0; j <= (nodename_string.Length - 1); j++)
//            {
//                geometry_dictionary_temp.Add(componentName + (nodename_string.GetValue(j)).ToString(), (j).ToString());
//            }
//            dictionaryOfGeoemtryNodes = geometry_dictionary_temp;
//        }

//        private void getDataFromTesTLab(string[] nodename_string, IGeometry DataOfGeoemetry, string componentName, out double[] node_x, out double[] node_y, out double[] node_z, out string[] line_start_string, out string[] line_end_string, out string[] Trias_start, out string[] Trias_middle, out string[] Trias_end, out string[] quads_1, out string[] quads_2, out string[] quads_3, out string[] quads_4)
//        {
//            Array line_start_array = null;
//            Array line_end_array = null;
//            Array Trias_start_array = null;
//            Array Trias_middle_array = null;
//            Array Trias_end_array = null;
//            Array quads_1_array = null;
//            Array quads_2_array = null;
//            Array quads_3_array = null;
//            Array quads_4_array = null;

//            if (ComponentNames.Length == 1 || componentName == Settings.Default.NotSpecifiedComponentName)
//            {
//                DataOfGeoemetry.Lines(out line_start_array, out line_end_array);
//                DataOfGeoemetry.Trias(out Trias_start_array, out Trias_middle_array, out Trias_end_array);
//                DataOfGeoemetry.Quads(out quads_1_array, out quads_2_array, out quads_3_array, out quads_4_array);
//                double[] node_x_t = new double[AllTheNodes.XNodes.Count];
//                double[] node_y_t = new double[AllTheNodes.YNodes.Count];
//                double[] node_z_t = new double[AllTheNodes.ZNodes.Count];
//                AllTheNodes.XNodes.CopyTo(node_x_t);
//                AllTheNodes.YNodes.CopyTo(node_y_t);
//                AllTheNodes.ZNodes.CopyTo(node_z_t);
//                node_x = node_x_t;
//                node_y = node_y_t;
//                node_z = node_z_t;

//            }
//            else
//            {
//                DataOfGeoemetry.ComponentNodesValues(componentName, nodename_string, out node_x_array, out node_y_array, out node_z_array, out node_xy, out node_xz, out node_yz, 0);
//                DataOfGeoemetry.ComponentLines(componentName, out line_start_array, out line_end_array);
//                DataOfGeoemetry.ComponentTrias(componentName, out Trias_start_array, out Trias_middle_array, out Trias_end_array);
//                DataOfGeoemetry.ComponentQuads(componentName, out quads_1_array, out quads_2_array, out quads_3_array, out quads_4_array);
//                node_x = (double[])node_x_array;
//                node_y = (double[])node_y_array;
//                node_z = (double[])node_z_array;
//            }


//            line_start_string = (string[])line_start_array;
//            line_end_string = (string[])line_end_array;
//            Trias_start = (string[])Trias_start_array;
//            Trias_middle = (string[])Trias_middle_array;
//            Trias_end = (string[])Trias_end_array;

//            quads_1 = (string[])quads_1_array;
//            quads_2 = (string[])quads_2_array;
//            quads_3 = (string[])quads_3_array;
//            quads_4 = (string[])quads_4_array;
//        }

//        private void GeneratingProjectName(out string ProjectName)
//        {
//            int ProjectNameWithExtension = Database.ProjectName.LastIndexOf('/');
//            ProjectName = Database.ProjectName.Substring(ProjectNameWithExtension + 1, (Database.ProjectName.Length - ProjectNameWithExtension - 5));
//        }

//        private void ChooseComponent(out int numberOfNode, out string[] NodesOfComponentNames, string componentName = null)
//        {
//            List<string> NamesOfTheNotSpecifiedComponent = new List<string>();
//            if (ComponentNames.Length == 1 || componentName == Settings.Default.NotSpecifiedComponentName)
//            {
//                int i = 0;
//                foreach (string value in ComponentNames)
//                {
//                    if (i < ComponentNames.Length - 1)
//                    {
//                        NamesOfTheNotSpecifiedComponent.AddRange((string[])DataOfGeoemetry.ComponentNodeNames[value]);
//                        DataOfGeoemetry.ComponentNodesValues(ComponentNames[i], (string[])DataOfGeoemetry.ComponentNodeNames[value], out node_x_array, out node_y_array, out node_z_array, out node_xy, out node_xz, out node_yz, 0);
//                        AllTheNodes.SetNodes(node_x_array, node_y_array, node_z_array);

//                    }
//                    else
//                    {
//                        NamesOfTheNotSpecifiedComponent.AddRange((string[])DataOfGeoemetry.NodeNames);
//                        DataOfGeoemetry.NodesValues((string[])DataOfGeoemetry.NodeNames, out node_x_array, out node_y_array, out node_z_array, out node_xy, out node_xz, out node_yz);
//                        AllTheNodes.SetNodes(node_x_array, node_y_array, node_z_array);

//                    }
//                    i++;
//                }
//                numberOfNode = NamesOfTheNotSpecifiedComponent.Count;
//                string[] InternalNamesofComponentNodes = new String[NamesOfTheNotSpecifiedComponent.Count];
//                NamesOfTheNotSpecifiedComponent.CopyTo(InternalNamesofComponentNodes);
//                NodesOfComponentNames = InternalNamesofComponentNodes;
//            }
//            else
//            {
//                //int keyIndex = Array.FindIndex(ComponentNames, w => w == componentName);
//                string[] NamesOfTheSpecifiedComponent = null;
//                NamesOfTheSpecifiedComponent = ((string[])DataOfGeoemetry.ComponentNodeNames[componentName]);
//                numberOfNode = NamesOfTheSpecifiedComponent.Length;
//                NodesOfComponentNames = NamesOfTheSpecifiedComponent;
//            }
//        }

//        private void reindexing_lines(Dictionary<string, string> geometry_dictionary, string componentName, double line_number, string[] line_start, string[] line_end, string[] nodenames, out double[] line_start_out, out double[] line_end_out)
//        {
//            double[] line_1_double = new double[Convert.ToUInt16(line_number)];
//            double[] line_2_double = new double[Convert.ToUInt16(line_number)];

//            for (int i_count = 0; i_count <= line_number - 1; i_count++)
//            {
//                if (componentName == Settings.Default.NotSpecifiedComponentName)
//                {
//                    string[] words = (line_end[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries);
//                    string word = words[words.Length - 1];

//                    line_1_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (line_start[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries)[(line_start[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries).Length - 1]]);
//                    line_2_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (line_end[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries)[(line_end[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries).Length - 1]]);

//                }
//                else
//                {
//                    line_1_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (line_start[i_count].ToString())]);
//                    line_2_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (line_end[i_count].ToString())]);

//                }
//            }

//            line_start_out = line_1_double;
//            line_end_out = line_2_double;
//        }

//        private void reindexing_trias(Dictionary<string, string> geometry_dictionary, string componentName, double trias_number, string[] trias_start, string[] trias_middle, string[] trias_end, string[] nodenames, out double[] trias_start_out, out double[] trias_middle_out, out double[] trias_end_out)
//        {
//            double[] trias_1_double = new double[Convert.ToUInt16(trias_number)];
//            double[] trias_2_double = new double[Convert.ToUInt16(trias_number)];
//            double[] trias_3_double = new double[Convert.ToUInt16(trias_number)];

//            for (int i_count = 0; i_count <= trias_number - 1; i_count++)
//            {
//                if (componentName == Settings.Default.GeometryDataName)
//                {
//                    trias_1_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (trias_start[i_count]).Split(separators, StringSplitOptions.RemoveEmptyEntries)[(trias_start[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries).Length - 1]]);
//                    trias_2_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (trias_middle[i_count]).Split(separators, StringSplitOptions.RemoveEmptyEntries)[(trias_middle[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries).Length - 1]]);
//                    trias_3_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (trias_end[i_count]).Split(separators, StringSplitOptions.RemoveEmptyEntries)[(trias_end[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries).Length - 1]]);

//                }
//                else
//                {
//                    trias_1_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (trias_start[i_count])]);
//                    trias_2_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (trias_middle[i_count])]);
//                    trias_3_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (trias_end[i_count])]);
//                }
//            }
//            trias_start_out = trias_1_double;
//            trias_middle_out = trias_2_double;
//            trias_end_out = trias_3_double;
//        }

//        private void reindexing_quads(Dictionary<string, string> geometry_dictionary, string componentName, double quads_number, string[] quads_1, string[] quads_2, string[] quads_3, string[] quads_4, string[] nodenames, out double[] quads_1_out, out double[] quads_2_out, out double[] quads_3_out, out double[] quads_4_out)
//        {
//            double[] quads_1_double = new double[Convert.ToUInt16(quads_number)];
//            double[] quads_2_double = new double[Convert.ToUInt16(quads_number)];
//            double[] quads_3_double = new double[Convert.ToUInt16(quads_number)];
//            double[] quads_4_double = new double[Convert.ToUInt16(quads_number)];

//            for (int i_count = 0; i_count <= quads_number - 1; i_count++)
//            {
//                if (componentName == Settings.Default.NotSpecifiedComponentName)
//                {

//                    quads_1_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (quads_1[i_count]).Split(separators, StringSplitOptions.RemoveEmptyEntries)[(quads_1[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries).Length - 1]]);
//                    quads_2_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (quads_2[i_count]).Split(separators, StringSplitOptions.RemoveEmptyEntries)[(quads_2[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries).Length - 1]]); ;
//                    quads_3_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (quads_3[i_count]).Split(separators, StringSplitOptions.RemoveEmptyEntries)[(quads_3[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries).Length - 1]]);
//                    quads_4_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (quads_4[i_count]).Split(separators, StringSplitOptions.RemoveEmptyEntries)[(quads_4[i_count].ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries).Length - 1]]);
//                }
//                else
//                {
//                    quads_1_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (quads_1[i_count])]);
//                    quads_2_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (quads_2[i_count])]);
//                    quads_3_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (quads_3[i_count])]);
//                    quads_4_double[i_count] = Convert.ToDouble(geometry_dictionary[componentName + (quads_4[i_count])]);
//                }


//            }
//            quads_1_out = quads_1_double;
//            quads_2_out = quads_2_double;
//            quads_3_out = quads_3_double;
//            quads_4_out = quads_4_double;
//        }

//        private void wireframe_nodes_string(int triacs_number, int quads_number, int line_number, int NumberOfNode, double[] node_x, double[] node_y, double[] node_z)
//        {
//            string[] objectfile_temp = new string[(line_number + 2 * NumberOfNode) + 1];
//            string[] objectfile_line_vectors = new string[2 * 4 * (line_number + NumberOfNode) + triacs_number + quads_number + 2];

//            string[] objectfile1_temp = new string[(line_number + 2 * NumberOfNode) + 1];
//            string[] objectfile2_temp = new string[(line_number + 2 * NumberOfNode) + 1];
//            string[] objectfile3_temp = new string[(line_number + 2 * NumberOfNode) + 1];
//            string[] objectfilemain_temp = new string[12 * line_number + NumberOfNode + triacs_number + quads_number + 2];

//            double thickness = 0.01;
//            if (line_start_class != null)
//            {
//                for (int i = 0; i < NumberOfNode; i++)
//                {
//                    objectfile_temp[i] = ("v" + " " + node_x[i].ToString() + " " + node_y[i].ToString() + " " + node_z[i].ToString());
//                    objectfilemain_temp[i] = objectfile_temp[i];


//                }
//                for (int i = 0; i < line_start_class.Length; i++)
//                {

//                    Vector3D beam_start = new Vector3D(node_x[(int)line_start_class[i]], node_y[(int)line_start_class[i]], node_z[(int)line_start_class[i]]);
//                    Vector3D beam_end = new Vector3D();
//                    beam_end = new Vector3D(node_x[(int)line_end_class[i]], node_y[(int)line_end_class[i]], node_z[(int)line_end_class[i]]);

//                    Vector3D beam_vector = Vector3D.Subtract(beam_end, beam_start);
//                    beam_vector.Normalize();
//                    Vector3D perpendicular_vector = new Vector3D(0, 0, 0);
//                    Vector3D perpendicular_vector_90 = new Vector3D(0, 0, 0);

//                    if (beam_vector.X == 0 && beam_vector.Y == 0)
//                    {
//                        perpendicular_vector = Vector3D.CrossProduct(beam_vector, new Vector3D(-beam_vector.Z, beam_vector.Y, beam_vector.X));

//                    }
//                    else
//                    {
//                        perpendicular_vector = Vector3D.CrossProduct(beam_vector, new Vector3D(-beam_vector.Y, beam_vector.X, beam_vector.Z));
//                    }
//                    perpendicular_vector.Normalize();
//                    perpendicular_vector = Vector3D.Multiply(thickness, perpendicular_vector);
//                    Vector3D vect1s = new Vector3D(0, 0, 0);
//                    Vector3D vect2s = new Vector3D(0, 0, 0);
//                    Vector3D vect3s = new Vector3D(0, 0, 0);
//                    Vector3D vect4s = new Vector3D(0, 0, 0);
//                    Vector3D vect1e = new Vector3D(0, 0, 0);
//                    Vector3D vect2e = new Vector3D(0, 0, 0);
//                    Vector3D vect3e = new Vector3D(0, 0, 0);
//                    Vector3D vect4e = new Vector3D(0, 0, 0);

//                    perpendicular_vector_90.X = (beam_vector.X * beam_vector.X) * perpendicular_vector.X + (beam_vector.X * beam_vector.Y - beam_vector.Z) * perpendicular_vector.Y + (beam_vector.X * beam_vector.Z - beam_vector.Y) * perpendicular_vector.Z;
//                    perpendicular_vector_90.Y = (beam_vector.X * beam_vector.Y + beam_vector.Z) * perpendicular_vector.X + (beam_vector.Y * beam_vector.Y) * perpendicular_vector.Y + (beam_vector.Y * beam_vector.Z - beam_vector.X) * perpendicular_vector.Z;
//                    perpendicular_vector_90.Z = (beam_vector.X * beam_vector.Z - beam_vector.Y) * perpendicular_vector.X + (beam_vector.Y * beam_vector.Z + beam_vector.X) * perpendicular_vector.Y + (beam_vector.Z * beam_vector.Z) * perpendicular_vector.Z;


//                    if (perpendicular_vector.X == 0 && perpendicular_vector.Y == 0)
//                    {
//                        vect1s = Vector3D.Add(perpendicular_vector, beam_start);
//                        vect3s = Vector3D.Add(-perpendicular_vector_90, beam_start);
//                        vect2s = Vector3D.Add(-perpendicular_vector, beam_start);
//                        vect4s = Vector3D.Add(perpendicular_vector_90, beam_start);

//                        vect1e = Vector3D.Add(perpendicular_vector, beam_end);
//                        vect3e = Vector3D.Add(-perpendicular_vector_90, beam_end);
//                        vect2e = Vector3D.Add(-perpendicular_vector, beam_end);
//                        vect4e = Vector3D.Add(perpendicular_vector_90, beam_end);
//                    }

//                    else
//                    {
//                        vect1s = Vector3D.Add(perpendicular_vector, beam_start);
//                        vect3s = Vector3D.Add(-perpendicular_vector_90, beam_start);
//                        vect2s = Vector3D.Add(-perpendicular_vector, beam_start);
//                        vect4s = Vector3D.Add(perpendicular_vector_90, beam_start);

//                        vect1e = Vector3D.Add(perpendicular_vector, beam_end);
//                        vect3e = Vector3D.Add(-perpendicular_vector_90, beam_end);
//                        vect2e = Vector3D.Add(-perpendicular_vector, beam_end);
//                        vect4e = Vector3D.Add(perpendicular_vector_90, beam_end);

//                    }

//                    objectfile_line_vectors[8 * i] = ("v" + " " + vect1s.X.ToString("F8") + " " + vect1s.Y.ToString("F8") + " " + vect1s.Z.ToString("F8"));
//                    objectfile_line_vectors[8 * i + 1] = ("v" + " " + vect2s.X.ToString("F8") + " " + vect2s.Y.ToString("F8") + " " + vect2s.Z.ToString("F8"));
//                    objectfile_line_vectors[8 * i + 2] = ("v" + " " + vect3s.X.ToString("F8") + " " + vect3s.Y.ToString("F8") + " " + vect3s.Z.ToString("F8"));
//                    objectfile_line_vectors[8 * i + 3] = ("v" + " " + vect4s.X.ToString("F8") + " " + vect4s.Y.ToString("F8") + " " + vect4s.Z.ToString("F8"));

//                    objectfile_line_vectors[8 * i + 4] = ("v" + " " + vect1e.X.ToString("F8") + " " + vect1e.Y.ToString("F8") + " " + vect1e.Z.ToString("F8"));
//                    objectfile_line_vectors[8 * i + 5] = ("v" + " " + vect2e.X.ToString("F8") + " " + vect2e.Y.ToString("F8") + " " + vect2e.Z.ToString("F8"));
//                    objectfile_line_vectors[8 * i + 6] = ("v" + " " + vect3e.X.ToString("F8") + " " + vect3e.Y.ToString("F8") + " " + vect3e.Z.ToString("F8"));
//                    objectfile_line_vectors[8 * i + 7] = ("v" + " " + vect4e.X.ToString("F8") + " " + vect4e.Y.ToString("F8") + " " + vect4e.Z.ToString("F8"));

//                    objectfilemain_temp[NumberOfNode + 8 * i] = objectfile_line_vectors[8 * i];
//                    objectfilemain_temp[NumberOfNode + 1 + 8 * i] = objectfile_line_vectors[8 * i + 1];
//                    objectfilemain_temp[NumberOfNode + 2 + 8 * i] = objectfile_line_vectors[8 * i + 2];
//                    objectfilemain_temp[NumberOfNode + 3 + 8 * i] = objectfile_line_vectors[8 * i + 3];
//                    objectfilemain_temp[NumberOfNode + 4 + 8 * i] = objectfile_line_vectors[8 * i + 4];
//                    objectfilemain_temp[NumberOfNode + 5 + 8 * i] = objectfile_line_vectors[8 * i + 5];
//                    objectfilemain_temp[NumberOfNode + 6 + 8 * i] = objectfile_line_vectors[8 * i + 6];
//                    objectfilemain_temp[NumberOfNode + 7 + 8 * i] = objectfile_line_vectors[8 * i + 7];

//                }

//                ObjectFilePart1 = objectfile_temp;
//                ObjectFilePart2 = objectfile1_temp;
//                ObjectFilePart3 = objectfile2_temp;
//                ObjectFilePart4 = objectfile3_temp;
//                ObjectFile = (objectfilemain_temp);
//            }
//        }

//        private void wireframe_lines_string(int triacs_number, int quads_number, int line_number, int NumberOfNode, double[] line_start, double[] line_end)
//        {
//            string[] objectfilemain_temp = new string[12 * line_number + NumberOfNode + triacs_number + quads_number + 2];
//            ObjectFile.CopyTo(objectfilemain_temp, 0);

//            for (int i = NumberOfNode + 1; i < 8 * line_number + NumberOfNode; i = i + 8)
//            {
//                ObjectFile[NumberOfNode / 2 + 8 * line_number + triacs_number + quads_number + i / 2 + 2] = ("f" + " " + (i).ToString() + " " + (i + 4).ToString() + " " + (i + 7).ToString() + " " + (i + 3).ToString());
//                ObjectFile[NumberOfNode / 2 + 8 * line_number + triacs_number + quads_number + i / 2 + 3] = ("f" + " " + (i + 1).ToString() + " " + (i + 5).ToString() + " " + (i + 6).ToString() + " " + (i + 2).ToString());
//                ObjectFile[NumberOfNode / 2 + 8 * line_number + triacs_number + quads_number + i / 2 + 4] = ("f" + " " + (i + 2).ToString() + " " + (i + 6).ToString() + " " + (i + 4).ToString() + " " + (i).ToString());
//                ObjectFile[NumberOfNode / 2 + 8 * line_number + triacs_number + quads_number + i / 2 + 5] = ("f" + " " + (i + 3).ToString() + " " + (i + 7).ToString() + " " + (i + 5).ToString() + " " + (i + 1).ToString());
//            }
//        }
//    }
//}
