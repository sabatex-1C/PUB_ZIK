using System;
using System.Windows.Forms;

namespace NTICS.DataGridViewCells
{
    public class DataGridViewSexSelectColumn : DataGridViewColumn
    {
        public DataGridViewSexSelectColumn()
            : base(new DataGridViewSexSelectCell())
    {
    }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewSexSelectCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }
    }


    public class DataGridViewSexSelectCell : DataGridViewTextBoxCell
    {

        public DataGridViewSexSelectCell() : base() { }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            SexSelectEditingControl ctl = DataGridView.EditingControl as SexSelectEditingControl;
            if (this.Value.GetType() != typeof(DBNull))
            ctl.SelectedIndex = (int)(short)(this.Value);
        }

        public override Type EditType
        {
            get
            {
                return typeof(SexSelectEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(short);
            }
        }

        public override Type FormattedValueType
        {
            get
            {
                return typeof(string);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return 0;
            }
        }
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if (value.GetType() == typeof(DBNull)) return "";
            return SexSelectEditingControl.Sex[int.Parse(value.ToString())];
        }
        public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter)
        {
            return formattedValue;
        }
    }


    class SexSelectEditingControl : ComboBox, IDataGridViewEditingControl
    {
        public static string[] Sex = { "×îë.", "Æ³í." };

        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

         
        public SexSelectEditingControl()
        {
            this.Items.Add(Sex[0]);
            this.Items.Add(Sex[1]);
        }

        protected override void OnSelectionChangeCommitted(EventArgs e)
        {
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnSelectionChangeCommitted(e);
        }
        //public object Value
        //{
        //    get { return this.SelectedIndex; }
        //    set { this.SelectedIndex = int.Parse(value.ToString()); }
        //}

        #region IDataGridViewEditingControl Members

        public object EditingControlFormattedValue
        {
            get
            {
                return (short)this.SelectedIndex; 
            }
            set
            {
                if (value is int)
                {
                    this.SelectedIndex = (int)value;
                }
            }
        }
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
        }
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }
        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return false;
            }
        }
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }
       

        #endregion
    }

    
}
