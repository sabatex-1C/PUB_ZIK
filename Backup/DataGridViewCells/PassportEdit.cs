using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PassportEditControl
{
    #region EditControl
    public partial class PassportEdit : UserControl, IDataGridViewEditingControl
    {
        private int _row;
        private DataGridView _dgv;
        private bool _valueChanged = false;
        private Passport _psForEditSession;

        public PassportEdit()
        {
            InitializeComponent();
        }

        private void _cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnValueChanged();
        }

        private void _dtp_ValueChanged(object sender, EventArgs e)
        {
            this.OnValueChanged();
        }

        private void _mskEdit_Validating(object sender, CancelEventArgs e)
        {
            if(this._mskEdit.MaskCompleted)
                this.OnValueChanged();
            e.Cancel = !this._mskEdit.MaskCompleted;
        }

        private void OnValueChanged()
        {
            this._valueChanged = true;
            this._psForEditSession.Series = (this._cb.SelectedItem == null ? string.Empty : this._cb.SelectedItem.ToString());
            this._psForEditSession.Number = (this._mskEdit.Text == null ? string.Empty : this._mskEdit.Text);
            this._psForEditSession.IssueDate = this._dtp.Value;
            DataGridView dgv = this.EditingControlDataGridView;
            if(dgv != null)
                dgv.NotifyCurrentCellDirty(true);
        }

        public void SetupControls(Passport ps)
        {
            this._psForEditSession = new Passport();
            this._cb.SelectedItem = ps.Series;
            this._mskEdit.Text = ps.Number;
            this._dtp.Value = ps.IssueDate;
        }

        #region IDataGridViewEditingControl Members

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this._cb.Font = this._mskEdit.Font = this._dtp.Font = dataGridViewCellStyle.Font;
            this.MinimumSize = this.Size;
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return this._dgv;
            }
            set
            {
                this._dgv = value;
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return this._psForEditSession;
            }
            set
            {
                //nothing to do...
            }
        }

        public int EditingControlRowIndex
        {
            get
            {
                return this._row;
            }
            set
            {
                this._row = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return this._valueChanged;
            }
            set
            {
                this._valueChanged = value;
            }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch(keyData & Keys.KeyCode)
            {
                case Keys.Down:
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Home:
                case Keys.End:
                    return true;
                default:
                    return false;
            }
        }

        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.EditingControlFormattedValue;
        }

        public void PrepareEditingControlForEdit(bool selectAll) { }

        public bool RepositionEditingControlOnValueChange { get { return false; } }

        #endregion
    }
    #endregion

    #region Custom Cell
    public class DataGridViewPassportCell : DataGridViewTextBoxCell
    {
        private const string DEFAULT_STRING = "Паспортные данные неизвестны...";
        private int _heightOfRowBeforeEditMode;

        public DataGridViewPassportCell() : base() { }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            PassportEdit pasCtrl = this.DataGridView.EditingControl as PassportEdit;
            this._heightOfRowBeforeEditMode = this.OwningRow.Height;
            this.OwningRow.Height = pasCtrl.Height;
            Passport pasInCell = this.Value as Passport;
            if(pasInCell == null)
                pasInCell = new Passport();
            pasCtrl.SetupControls(pasInCell);
        }

        public override void DetachEditingControl()
        {
            if(this._heightOfRowBeforeEditMode > 0)
                this.OwningRow.Height = this._heightOfRowBeforeEditMode;
            base.DetachEditingControl();
        }

        public override Type EditType
        {
            get
            {
                return typeof(PassportEdit);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(Passport);
            }
        }

        public override Type FormattedValueType
        {
            get
            {
                return typeof(Passport);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return DEFAULT_STRING;
            }
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            if(value == null)
                return DEFAULT_STRING;
            else
                return TypeDescriptor.GetConverter(value).ConvertToString(value);
        }
    }
    #endregion

    #region Custom Column
    public class DataGridViewPassportColumn : DataGridViewColumn
    {
        public DataGridViewPassportColumn() : base(new DataGridViewPassportCell()) { }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if(value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewPassportCell)))
                    throw new InvalidCastException("Cell must be a PassportCell");
                base.CellTemplate = value;
            }
        }
    }
    #endregion

    #region Passport object
    [TypeConverter(typeof(PassportConverter))]
    public class Passport
    {
        private string _series;
        private string _number;
        private DateTime _issueDate;

        public string Series
        {
            get
            {
                return _series;
            }
            set
            {
                if(!string.IsNullOrEmpty(value) && value.Length != 2)
                    throw new ArgumentOutOfRangeException("value", "Series must contain exactly 2 digits");
                else if(!string.IsNullOrEmpty(value))
                {
                    uint result;
                    if(!uint.TryParse(value, out result))
                        throw new ArgumentOutOfRangeException("value", "Series must contain only digits");
                    if(result != 0 && result % 10 != result / 10)
                        throw new ArgumentOutOfRangeException("value", "Series must folow template - 00, 11, 22, ...");
                }
                _series = value;
            }
        }
        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                if(!string.IsNullOrEmpty(value) && value.Length != 6)
                    throw new ArgumentOutOfRangeException("value", "Series must contain exactly 6 digits");
                else if(!string.IsNullOrEmpty(value))
                {
                    uint result;
                    if(!uint.TryParse(value, out result))
                        throw new ArgumentOutOfRangeException("value", "Series must contain only digits");
                }
                _number = value;
            }
        }
        public DateTime IssueDate
        {
            get
            {
                return _issueDate;
            }
            set
            {
                _issueDate = value;
            }
        }


        /// <summary>
        /// Creates a new instance of Passport
        /// </summary>
        /// <param name="series">Passport Series</param>
        /// <param name="number">Number of Passport</param>
        /// <param name="issueDate">Date of issue</param>
        public Passport(string series, string number, DateTime issueDate)
        {
            this.Series = series;
            this.Number = number;
            this.IssueDate = issueDate;
        }


        /// <summary>
        /// Creates a new instance of "default" Passport
        /// </summary>
        public Passport() : this("00", "000000", new DateTime(1970, 1, 1)) { }
    }
    #endregion

    #region PassportConverter
    public class PassportConverter : TypeConverter
    {
        private const string DEFAULT_FORMAT_STRING = "Серия: {0} " + " №: {1} " + " Выдан: {2} ";

        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if(destinationType == typeof(string))
            {
                Passport pas = value as Passport;
                return string.Format(DEFAULT_FORMAT_STRING, pas.Series, pas.Number, pas.IssueDate.ToString("d"));
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
    #endregion
}