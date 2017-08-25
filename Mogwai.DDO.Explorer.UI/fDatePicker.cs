using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mogwai.DDO.Explorer.UI
{
    public partial class fDatePicker : Form
    {
        public DateTime? SelectedDate { get; set; } = null;

        public DateFilterOptions? SelectedOption { get; set; } = null;

        public fDatePicker()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedDate = datePicker.Value;
            SelectedOption = ((dynamic)(DateFilterOptions)cbOption.SelectedItem).Key;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fDatePicker_Load(object sender, EventArgs e)
        {
            cbOption.DataSource = Enum.GetValues(typeof(DateFilterOptions))
                .Cast<DateFilterOptions>()
                .Select(p => new { Key = (int)p, Value = EnumHelpers.GetEnumDescription(p) })
                .ToList();
            cbOption.SelectedIndex = 0;
        }
    }
}
