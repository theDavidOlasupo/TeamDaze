using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamDaze.BLL.DAL;

namespace TeamDaze.Enrollment
{
    public partial class Report : Form
    {
        private IncidentRepository incidentRepository;
        public Report()
        {
            InitializeComponent();
            incidentRepository = new IncidentRepository();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            DataSet result = incidentRepository.GetIncident();
            var source = new BindingSource(result.Tables[0], null);
            dataGridView1.DataSource = source;
          

        }
    }
}
