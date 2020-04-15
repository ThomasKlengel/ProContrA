using System.Windows;

namespace ProContrA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ModelParser.Initializer.Initialize();
            //Mapping.ReadMappingFromFile();
            //// Mapping.WriteRawMappingToFile();
            //var m = Model.Conditions.Globals.Mapping;
            //var l = Model.Conditions.Logger.Logs;

            //var avg = 0.0;

            //for (int i = 0; i < 1000; i++)
            //{
            //    Dictionary<string, bool> currentEvaluation = new Dictionary<string, bool>();

            //    var t1 = DateTime.Now;
            //    foreach (var cond in Model.Conditions.Globals.Conditions)
            //    {
            //        var res = Model.Conditions.Evaluation.Evaluator.Evaluate(cond.Value);

            //        if (currentEvaluation.ContainsKey(cond.Value.Value))
            //        {
            //            currentEvaluation[cond.Value.Value] = res;
            //        }
            //        else
            //        {
            //            currentEvaluation.Add(cond.Value.Value, res);
            //        }
            //    }
            //    var t2 = DateTime.Now;
            //    var ts = t2 - t1;
            //    avg += ts.TotalMilliseconds;
            //}
            //avg = avg / 1000.0; // avg 400ms im debugging, 0.4ms als exe

            //MessageBox.Show($"Elapsed ms for 100 evalutaions = {avg}");
        }
    }
}
