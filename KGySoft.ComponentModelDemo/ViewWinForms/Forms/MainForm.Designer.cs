using System.Windows.Forms;
using KGySoft.ComponentModelDemo.ViewWinForms.Controls;

namespace KGySoft.ComponentModelDemo.ViewWinForms.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbInnerObservableBindingList = new System.Windows.Forms.RadioButton();
            this.rbInnerObservableCollection = new System.Windows.Forms.RadioButton();
            this.rbInnerSortableBindingList = new System.Windows.Forms.RadioButton();
            this.rbInnerBindingList = new System.Windows.Forms.RadioButton();
            this.rbInnerList = new System.Windows.Forms.RadioButton();
            this.rbNoInnerList = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbModel = new System.Windows.Forms.RadioButton();
            this.rbValidating = new System.Windows.Forms.RadioButton();
            this.rbEditable = new System.Windows.Forms.RadioButton();
            this.rbUndoable = new System.Windows.Forms.RadioButton();
            this.rbObservableObject = new System.Windows.Forms.RadioButton();
            this.rbObject = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbObservableBindingList = new System.Windows.Forms.RadioButton();
            this.rbObservableCollection = new System.Windows.Forms.RadioButton();
            this.rbSortableBindingListSortOnChange = new System.Windows.Forms.RadioButton();
            this.rbSortableBindingList = new System.Windows.Forms.RadioButton();
            this.rbBindingList = new System.Windows.Forms.RadioButton();
            this.rbList = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gbBoundToCurrentItem = new System.Windows.Forms.GroupBox();
            this.tbStringPropCurrent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbIntPropCurrent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbBoundToList = new System.Windows.Forms.GroupBox();
            this.tbStringPropList = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbIntPropList = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbGrid = new System.Windows.Forms.GroupBox();
            this.grid = new KGySoft.ComponentModelDemo.ViewWinForms.Controls.SafeDataGridView();
            this.intPropDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stringPropDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gbListBox = new System.Windows.Forms.GroupBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.tsList = new System.Windows.Forms.ToolStrip();
            this.btnChangeInner = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnRemove = new System.Windows.Forms.ToolStripButton();
            this.btnSetItem = new System.Windows.Forms.ToolStripButton();
            this.btnSetProp = new System.Windows.Forms.ToolStripButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.editMenuStrip = new KGySoft.ComponentModelDemo.ViewWinForms.Controls.EditMenuStrip();
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.warningProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.infoProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.warningAdapter = new KGySoft.ComponentModelDemo.ViewWinForms.Components.ValidationResultToErrorProviderAdapter(this.components);
            this.infoAdapter = new KGySoft.ComponentModelDemo.ViewWinForms.Components.ValidationResultToErrorProviderAdapter(this.components);
            this.lblInfo = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbBoundToCurrentItem.SuspendLayout();
            this.gbBoundToList.SuspendLayout();
            this.gbGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBindingSource)).BeginInit();
            this.gbListBox.SuspendLayout();
            this.tsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.warningProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel1.Location = new System.Drawing.Point(0, 53);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(684, 134);
            this.Panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 134);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbInnerObservableBindingList);
            this.groupBox3.Controls.Add(this.rbInnerObservableCollection);
            this.groupBox3.Controls.Add(this.rbInnerSortableBindingList);
            this.groupBox3.Controls.Add(this.rbInnerBindingList);
            this.groupBox3.Controls.Add(this.rbInnerList);
            this.groupBox3.Controls.Add(this.rbNoInnerList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(231, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(222, 128);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Wrapped List Type";
            this.toolTip.SetToolTip(this.groupBox3, resources.GetString("groupBox3.ToolTip"));
            // 
            // rbInnerObservableBindingList
            // 
            this.rbInnerObservableBindingList.AutoSize = true;
            this.rbInnerObservableBindingList.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbInnerObservableBindingList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbInnerObservableBindingList.Location = new System.Drawing.Point(3, 106);
            this.rbInnerObservableBindingList.Name = "rbInnerObservableBindingList";
            this.rbInnerObservableBindingList.Size = new System.Drawing.Size(216, 18);
            this.rbInnerObservableBindingList.TabIndex = 5;
            this.rbInnerObservableBindingList.Text = "ObservableBindingList";
            this.toolTip.SetToolTip(this.rbInnerObservableBindingList, "If wrapped into an ObservableBindingList, then direct modifications on the wrappe" +
        "d list are detected.\r\n• Supports sorting if created by its default constructor");
            this.rbInnerObservableBindingList.UseVisualStyleBackColor = true;
            // 
            // rbInnerObservableCollection
            // 
            this.rbInnerObservableCollection.AutoSize = true;
            this.rbInnerObservableCollection.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbInnerObservableCollection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbInnerObservableCollection.Location = new System.Drawing.Point(3, 88);
            this.rbInnerObservableCollection.Name = "rbInnerObservableCollection";
            this.rbInnerObservableCollection.Size = new System.Drawing.Size(216, 18);
            this.rbInnerObservableCollection.TabIndex = 4;
            this.rbInnerObservableCollection.Text = "ObservableCollection";
            this.toolTip.SetToolTip(this.rbInnerObservableCollection, "If wrapped into an ObservableBindingList, then direct modifications on the wrappe" +
        "d list are detected.\r\n• Does not support sorting");
            this.rbInnerObservableCollection.UseVisualStyleBackColor = true;
            // 
            // rbInnerSortableBindingList
            // 
            this.rbInnerSortableBindingList.AutoSize = true;
            this.rbInnerSortableBindingList.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbInnerSortableBindingList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbInnerSortableBindingList.Location = new System.Drawing.Point(3, 70);
            this.rbInnerSortableBindingList.Name = "rbInnerSortableBindingList";
            this.rbInnerSortableBindingList.Size = new System.Drawing.Size(216, 18);
            this.rbInnerSortableBindingList.TabIndex = 3;
            this.rbInnerSortableBindingList.Text = "SortableBindingList";
            this.toolTip.SetToolTip(this.rbInnerSortableBindingList, "If wrapped into an ObservableBindingList, then direct modifications on the wrappe" +
        "d list are detected.\r\n• Supports sorting");
            this.rbInnerSortableBindingList.UseVisualStyleBackColor = true;
            // 
            // rbInnerBindingList
            // 
            this.rbInnerBindingList.AutoSize = true;
            this.rbInnerBindingList.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbInnerBindingList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbInnerBindingList.Location = new System.Drawing.Point(3, 52);
            this.rbInnerBindingList.Name = "rbInnerBindingList";
            this.rbInnerBindingList.Size = new System.Drawing.Size(216, 18);
            this.rbInnerBindingList.TabIndex = 2;
            this.rbInnerBindingList.Text = "BindingList";
            this.toolTip.SetToolTip(this.rbInnerBindingList, "If wrapped into an ObservableBindingList, then direct modifications on the wrappe" +
        "d list are detected.\r\n• Does not support sorting");
            this.rbInnerBindingList.UseVisualStyleBackColor = true;
            // 
            // rbInnerList
            // 
            this.rbInnerList.AutoSize = true;
            this.rbInnerList.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbInnerList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbInnerList.Location = new System.Drawing.Point(3, 34);
            this.rbInnerList.Name = "rbInnerList";
            this.rbInnerList.Size = new System.Drawing.Size(216, 18);
            this.rbInnerList.TabIndex = 1;
            this.rbInnerList.Text = "List";
            this.toolTip.SetToolTip(this.rbInnerList, "A List is passed to the default constructor.\r\n• Has no notification events so dir" +
        "ect changes on the wrapped collection are not detected");
            this.rbInnerList.UseVisualStyleBackColor = true;
            // 
            // rbNoInnerList
            // 
            this.rbNoInnerList.AutoSize = true;
            this.rbNoInnerList.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbNoInnerList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbNoInnerList.Location = new System.Drawing.Point(3, 16);
            this.rbNoInnerList.Name = "rbNoInnerList";
            this.rbNoInnerList.Size = new System.Drawing.Size(216, 18);
            this.rbNoInnerList.TabIndex = 0;
            this.rbNoInnerList.Text = "None";
            this.toolTip.SetToolTip(this.rbNoInnerList, "The bound list is created by its default constructor.");
            this.rbNoInnerList.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbModel);
            this.groupBox2.Controls.Add(this.rbValidating);
            this.groupBox2.Controls.Add(this.rbEditable);
            this.groupBox2.Controls.Add(this.rbUndoable);
            this.groupBox2.Controls.Add(this.rbObservableObject);
            this.groupBox2.Controls.Add(this.rbObject);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(459, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(222, 128);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Element Type";
            this.toolTip.SetToolTip(this.groupBox2, "Determined the element type of the created collection");
            // 
            // rbModel
            // 
            this.rbModel.AutoSize = true;
            this.rbModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbModel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbModel.Location = new System.Drawing.Point(3, 106);
            this.rbModel.Name = "rbModel";
            this.rbModel.Size = new System.Drawing.Size(216, 18);
            this.rbModel.TabIndex = 5;
            this.rbModel.Text = "AllInOneTestObject";
            this.toolTip.SetToolTip(this.rbModel, "The element type is derived from ModelBase\r\n• Supports property change notificati" +
        "on\r\n• Supports Undo/Redo\r\n• Supports BeginEdit/EndEdit/CancelEdit\r\n• Supports Va" +
        "lidation");
            this.rbModel.UseVisualStyleBackColor = true;
            // 
            // rbValidating
            // 
            this.rbValidating.AutoSize = true;
            this.rbValidating.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbValidating.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbValidating.Location = new System.Drawing.Point(3, 88);
            this.rbValidating.Name = "rbValidating";
            this.rbValidating.Size = new System.Drawing.Size(216, 18);
            this.rbValidating.TabIndex = 4;
            this.rbValidating.Text = "ValidatingTestObject";
            this.toolTip.SetToolTip(this.rbValidating, "The element type is derived from ValidatingObjectBase\r\n• Supports property change" +
        " notification\r\n• Supports Validation");
            this.rbValidating.UseVisualStyleBackColor = true;
            // 
            // rbEditable
            // 
            this.rbEditable.AutoSize = true;
            this.rbEditable.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbEditable.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbEditable.Location = new System.Drawing.Point(3, 70);
            this.rbEditable.Name = "rbEditable";
            this.rbEditable.Size = new System.Drawing.Size(216, 18);
            this.rbEditable.TabIndex = 3;
            this.rbEditable.Text = "EditableTestObject";
            this.toolTip.SetToolTip(this.rbEditable, "The element type is derived from EditableObjectBase\r\n• Supports property change n" +
        "otification\r\n• Supports BeginEdit/EndEdit/CancelEdit");
            this.rbEditable.UseVisualStyleBackColor = true;
            // 
            // rbUndoable
            // 
            this.rbUndoable.AutoSize = true;
            this.rbUndoable.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbUndoable.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbUndoable.Location = new System.Drawing.Point(3, 52);
            this.rbUndoable.Name = "rbUndoable";
            this.rbUndoable.Size = new System.Drawing.Size(216, 18);
            this.rbUndoable.TabIndex = 2;
            this.rbUndoable.Text = "UndoableTestObject";
            this.toolTip.SetToolTip(this.rbUndoable, "The element type is derived from UndoableObjectBase\r\n• Supports property change n" +
        "otification\r\n• Supports Undo/Redo");
            this.rbUndoable.UseVisualStyleBackColor = true;
            // 
            // rbObservableObject
            // 
            this.rbObservableObject.AutoSize = true;
            this.rbObservableObject.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbObservableObject.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbObservableObject.Location = new System.Drawing.Point(3, 34);
            this.rbObservableObject.Name = "rbObservableObject";
            this.rbObservableObject.Size = new System.Drawing.Size(216, 18);
            this.rbObservableObject.TabIndex = 1;
            this.rbObservableObject.Text = "ObservableTestObject";
            this.toolTip.SetToolTip(this.rbObservableObject, "The element type is derived from ObservableObjectBase\r\n• Supports property change" +
        " notification");
            this.rbObservableObject.UseVisualStyleBackColor = true;
            // 
            // rbObject
            // 
            this.rbObject.AutoSize = true;
            this.rbObject.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbObject.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbObject.Location = new System.Drawing.Point(3, 16);
            this.rbObject.Name = "rbObject";
            this.rbObject.Size = new System.Drawing.Size(216, 18);
            this.rbObject.TabIndex = 0;
            this.rbObject.Text = "PlainTestObject";
            this.toolTip.SetToolTip(this.rbObject, "The element type is derived from object\r\n• Does not support property change notif" +
        "ication");
            this.rbObject.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbObservableBindingList);
            this.groupBox1.Controls.Add(this.rbObservableCollection);
            this.groupBox1.Controls.Add(this.rbSortableBindingListSortOnChange);
            this.groupBox1.Controls.Add(this.rbSortableBindingList);
            this.groupBox1.Controls.Add(this.rbBindingList);
            this.groupBox1.Controls.Add(this.rbList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bound List Type";
            this.toolTip.SetToolTip(this.groupBox1, "The list type to bind.\r\n• List has no notification events so direct changes on it" +
        " cannot be detected\r\n• ObservableCollection is not supported by Windows Forms");
            // 
            // rbObservableBindingList
            // 
            this.rbObservableBindingList.AutoSize = true;
            this.rbObservableBindingList.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbObservableBindingList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbObservableBindingList.Location = new System.Drawing.Point(3, 106);
            this.rbObservableBindingList.Name = "rbObservableBindingList";
            this.rbObservableBindingList.Size = new System.Drawing.Size(216, 18);
            this.rbObservableBindingList.TabIndex = 5;
            this.rbObservableBindingList.Text = "ObservableBindingList";
            this.toolTip.SetToolTip(this.rbObservableBindingList, resources.GetString("rbObservableBindingList.ToolTip"));
            this.rbObservableBindingList.UseVisualStyleBackColor = true;
            // 
            // rbObservableCollection
            // 
            this.rbObservableCollection.AutoSize = true;
            this.rbObservableCollection.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbObservableCollection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbObservableCollection.Location = new System.Drawing.Point(3, 88);
            this.rbObservableCollection.Name = "rbObservableCollection";
            this.rbObservableCollection.Size = new System.Drawing.Size(216, 18);
            this.rbObservableCollection.TabIndex = 4;
            this.rbObservableCollection.Text = "ObservableCollection";
            this.toolTip.SetToolTip(this.rbObservableCollection, "• Not supported by Windows Forms directly but can be wrapped into an ObservableBi" +
        "ndingList\r\n• Cannot wrap a list, the constructor just copies the elements\r\n• Doe" +
        "s not support sorting");
            this.rbObservableCollection.UseVisualStyleBackColor = true;
            // 
            // rbSortableBindingListSortOnChange
            // 
            this.rbSortableBindingListSortOnChange.AutoSize = true;
            this.rbSortableBindingListSortOnChange.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbSortableBindingListSortOnChange.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbSortableBindingListSortOnChange.Location = new System.Drawing.Point(3, 70);
            this.rbSortableBindingListSortOnChange.Name = "rbSortableBindingListSortOnChange";
            this.rbSortableBindingListSortOnChange.Size = new System.Drawing.Size(216, 18);
            this.rbSortableBindingListSortOnChange.TabIndex = 3;
            this.rbSortableBindingListSortOnChange.Text = "SortableBindingList (SortOnChange)";
            this.toolTip.SetToolTip(this.rbSortableBindingListSortOnChange, resources.GetString("rbSortableBindingListSortOnChange.ToolTip"));
            this.rbSortableBindingListSortOnChange.UseVisualStyleBackColor = true;
            // 
            // rbSortableBindingList
            // 
            this.rbSortableBindingList.AutoSize = true;
            this.rbSortableBindingList.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbSortableBindingList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbSortableBindingList.Location = new System.Drawing.Point(3, 52);
            this.rbSortableBindingList.Name = "rbSortableBindingList";
            this.rbSortableBindingList.Size = new System.Drawing.Size(216, 18);
            this.rbSortableBindingList.TabIndex = 2;
            this.rbSortableBindingList.Text = "SortableBindingList";
            this.toolTip.SetToolTip(this.rbSortableBindingList, "• Events of a wrapped list are not captured\r\n• Events of item property changes ar" +
        "e captured\r\n• Does not re-sort items when they are edited");
            this.rbSortableBindingList.UseVisualStyleBackColor = true;
            // 
            // rbBindingList
            // 
            this.rbBindingList.AutoSize = true;
            this.rbBindingList.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbBindingList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbBindingList.Location = new System.Drawing.Point(3, 34);
            this.rbBindingList.Name = "rbBindingList";
            this.rbBindingList.Size = new System.Drawing.Size(216, 18);
            this.rbBindingList.TabIndex = 1;
            this.rbBindingList.Text = "BindingList";
            this.toolTip.SetToolTip(this.rbBindingList, "• Does not support sorting\r\n• Events of a wrapped list are not captured\r\n• Events" +
        " of item property changes are captured");
            this.rbBindingList.UseVisualStyleBackColor = true;
            // 
            // rbList
            // 
            this.rbList.AutoSize = true;
            this.rbList.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbList.Location = new System.Drawing.Point(3, 16);
            this.rbList.Name = "rbList";
            this.rbList.Size = new System.Drawing.Size(216, 18);
            this.rbList.TabIndex = 0;
            this.rbList.Text = "List";
            this.toolTip.SetToolTip(this.rbList, "• Has no notification events so direct changes are not reflected until refreshing" +
        ".\r\n• Cannot wrap a list, the constructor just copies the elements\r\n• Does not su" +
        "pport sorting");
            this.rbList.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.gbBoundToCurrentItem, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.gbBoundToList, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.gbGrid, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.gbListBox, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(40, 187);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(620, 254);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // gbBoundToCurrentItem
            // 
            this.gbBoundToCurrentItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBoundToCurrentItem.Controls.Add(this.tbStringPropCurrent);
            this.gbBoundToCurrentItem.Controls.Add(this.label3);
            this.gbBoundToCurrentItem.Controls.Add(this.tbIntPropCurrent);
            this.gbBoundToCurrentItem.Controls.Add(this.label4);
            this.gbBoundToCurrentItem.Location = new System.Drawing.Point(313, 130);
            this.gbBoundToCurrentItem.Name = "gbBoundToCurrentItem";
            this.gbBoundToCurrentItem.Size = new System.Drawing.Size(304, 121);
            this.gbBoundToCurrentItem.TabIndex = 3;
            this.gbBoundToCurrentItem.TabStop = false;
            this.gbBoundToCurrentItem.Text = "Bound to the current item of the binding source";
            // 
            // tbStringPropCurrent
            // 
            this.tbStringPropCurrent.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbStringPropCurrent.Location = new System.Drawing.Point(3, 62);
            this.tbStringPropCurrent.Name = "tbStringPropCurrent";
            this.tbStringPropCurrent.Size = new System.Drawing.Size(298, 20);
            this.tbStringPropCurrent.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(3, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "StringProp";
            // 
            // tbIntPropCurrent
            // 
            this.tbIntPropCurrent.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbIntPropCurrent.Location = new System.Drawing.Point(3, 29);
            this.tbIntPropCurrent.Name = "tbIntPropCurrent";
            this.tbIntPropCurrent.Size = new System.Drawing.Size(298, 20);
            this.tbIntPropCurrent.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(3, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "IntProp";
            // 
            // gbBoundToList
            // 
            this.gbBoundToList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbBoundToList.Controls.Add(this.tbStringPropList);
            this.gbBoundToList.Controls.Add(this.label2);
            this.gbBoundToList.Controls.Add(this.tbIntPropList);
            this.gbBoundToList.Controls.Add(this.label1);
            this.gbBoundToList.Location = new System.Drawing.Point(313, 3);
            this.gbBoundToList.Name = "gbBoundToList";
            this.gbBoundToList.Size = new System.Drawing.Size(304, 121);
            this.gbBoundToList.TabIndex = 2;
            this.gbBoundToList.TabStop = false;
            this.gbBoundToList.Text = "Bound to the same source as the Grid and ListBox";
            // 
            // tbStringPropList
            // 
            this.tbStringPropList.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbStringPropList.Location = new System.Drawing.Point(3, 62);
            this.tbStringPropList.Name = "tbStringPropList";
            this.tbStringPropList.Size = new System.Drawing.Size(298, 20);
            this.tbStringPropList.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "StringProp";
            // 
            // tbIntPropList
            // 
            this.tbIntPropList.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbIntPropList.Location = new System.Drawing.Point(3, 29);
            this.tbIntPropList.Name = "tbIntPropList";
            this.tbIntPropList.Size = new System.Drawing.Size(298, 20);
            this.tbIntPropList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IntProp";
            // 
            // gbGrid
            // 
            this.gbGrid.Controls.Add(this.grid);
            this.gbGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbGrid.Location = new System.Drawing.Point(3, 3);
            this.gbGrid.Name = "gbGrid";
            this.gbGrid.Size = new System.Drawing.Size(304, 121);
            this.gbGrid.TabIndex = 4;
            this.gbGrid.TabStop = false;
            this.gbGrid.Text = "DataGridView";
            // 
            // grid
            // 
            this.grid.AutoGenerateColumns = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.intPropDataGridViewTextBoxColumn,
            this.stringPropDataGridViewTextBoxColumn});
            this.grid.DataSource = this.listBindingSource;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(3, 16);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(298, 102);
            this.grid.TabIndex = 1;
            // 
            // intPropDataGridViewTextBoxColumn
            // 
            this.intPropDataGridViewTextBoxColumn.DataPropertyName = "IntProp";
            this.intPropDataGridViewTextBoxColumn.HeaderText = "IntProp";
            this.intPropDataGridViewTextBoxColumn.Name = "intPropDataGridViewTextBoxColumn";
            // 
            // stringPropDataGridViewTextBoxColumn
            // 
            this.stringPropDataGridViewTextBoxColumn.DataPropertyName = "StringProp";
            this.stringPropDataGridViewTextBoxColumn.HeaderText = "StringProp";
            this.stringPropDataGridViewTextBoxColumn.Name = "stringPropDataGridViewTextBoxColumn";
            // 
            // listBindingSource
            // 
            this.listBindingSource.DataSource = typeof(KGySoft.ComponentModelDemo.Model.ITestObject);
            // 
            // gbListBox
            // 
            this.gbListBox.Controls.Add(this.listBox);
            this.gbListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbListBox.Location = new System.Drawing.Point(3, 130);
            this.gbListBox.Name = "gbListBox";
            this.gbListBox.Size = new System.Drawing.Size(304, 121);
            this.gbListBox.TabIndex = 5;
            this.gbListBox.TabStop = false;
            this.gbListBox.Text = "ListBox";
            // 
            // listBox
            // 
            this.listBox.DataSource = this.listBindingSource;
            this.listBox.DisplayMember = "StringProp";
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(3, 16);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(298, 102);
            this.listBox.TabIndex = 2;
            // 
            // tsList
            // 
            this.tsList.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnChangeInner,
            this.btnReset,
            this.btnAdd,
            this.btnRemove,
            this.btnSetItem,
            this.btnSetProp});
            this.tsList.Location = new System.Drawing.Point(0, 187);
            this.tsList.Name = "tsList";
            this.tsList.Size = new System.Drawing.Size(40, 254);
            this.tsList.TabIndex = 0;
            this.tsList.Text = "toolStrip1";
            // 
            // btnChangeInner
            // 
            this.btnChangeInner.CheckOnClick = true;
            this.btnChangeInner.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnChangeInner.Image = ((System.Drawing.Image)(resources.GetObject("btnChangeInner.Image")));
            this.btnChangeInner.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChangeInner.Name = "btnChangeInner";
            this.btnChangeInner.Size = new System.Drawing.Size(37, 19);
            this.btnChangeInner.Text = "[...]";
            this.btnChangeInner.ToolTipText = resources.GetString("btnChangeInner.ToolTipText");
            // 
            // btnReset
            // 
            this.btnReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(37, 19);
            this.btnReset.Text = "Reset";
            this.btnReset.ToolTipText = "Resets the binding. Press to re-read the bound list if it was updated and the cha" +
    "nges could not be detected.";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(37, 19);
            this.btnAdd.Text = "Add";
            this.btnAdd.ToolTipText = resources.GetString("btnAdd.ToolTipText");
            // 
            // btnRemove
            // 
            this.btnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(37, 19);
            this.btnRemove.Text = "Del";
            this.btnRemove.ToolTipText = resources.GetString("btnRemove.ToolTipText");
            // 
            // btnSetItem
            // 
            this.btnSetItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSetItem.Image = ((System.Drawing.Image)(resources.GetObject("btnSetItem.Image")));
            this.btnSetItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetItem.Name = "btnSetItem";
            this.btnSetItem.Size = new System.Drawing.Size(37, 19);
            this.btnSetItem.Text = "Item";
            this.btnSetItem.ToolTipText = resources.GetString("btnSetItem.ToolTipText");
            // 
            // btnSetProp
            // 
            this.btnSetProp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSetProp.Image = ((System.Drawing.Image)(resources.GetObject("btnSetProp.Image")));
            this.btnSetProp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetProp.Name = "btnSetProp";
            this.btnSetProp.Size = new System.Drawing.Size(37, 19);
            this.btnSetProp.Text = "Prop";
            this.btnSetProp.ToolTipText = resources.GetString("btnSetProp.ToolTipText");
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.DataSource = this.listBindingSource;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // editMenuStrip
            // 
            this.editMenuStrip.DataSource = null;
            this.editMenuStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.editMenuStrip.Location = new System.Drawing.Point(660, 187);
            this.editMenuStrip.Name = "editMenuStrip";
            this.editMenuStrip.Size = new System.Drawing.Size(24, 254);
            this.editMenuStrip.TabIndex = 2;
            this.editMenuStrip.Text = "editMenuStrip1";
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(KGySoft.ComponentModelDemo.Model.ITestObject);
            // 
            // warningProvider
            // 
            this.warningProvider.ContainerControl = this;
            // 
            // infoProvider
            // 
            this.infoProvider.ContainerControl = this;
            // 
            // warningAdapter
            // 
            this.warningAdapter.DataSource = this.listBindingSource;
            this.warningAdapter.Provider = this.warningProvider;
            this.warningAdapter.Severity = KGySoft.ComponentModel.ValidationSeverity.Warning;
            // 
            // infoAdapter
            // 
            this.infoAdapter.DataSource = this.listBindingSource;
            this.infoAdapter.Provider = this.infoProvider;
            this.infoAdapter.Severity = KGySoft.ComponentModel.ValidationSeverity.Information;
            // 
            // lblInfo
            // 
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Padding = new System.Windows.Forms.Padding(2);
            this.lblInfo.Size = new System.Drawing.Size(684, 53);
            this.lblInfo.TabIndex = 3;
            this.lblInfo.Text = resources.GetString("lblInfo.Text");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 441);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.editMenuStrip);
            this.Controls.Add(this.tsList);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.lblInfo);
            this.MinimumSize = new System.Drawing.Size(690, 450);
            this.Name = "MainForm";
            this.Text = "KGySoft.ComponentModel Demo (Windows Forms)";
            this.Panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.gbBoundToCurrentItem.ResumeLayout(false);
            this.gbBoundToCurrentItem.PerformLayout();
            this.gbBoundToList.ResumeLayout(false);
            this.gbBoundToList.PerformLayout();
            this.gbGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBindingSource)).EndInit();
            this.gbListBox.ResumeLayout(false);
            this.tsList.ResumeLayout(false);
            this.tsList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.warningProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.infoProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel Panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private RadioButton rbBindingList;
        private RadioButton rbList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private RadioButton rbSortableBindingList;
        private System.Windows.Forms.ToolStrip tsList;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnRemove;
        private System.Windows.Forms.ToolStripButton btnSetItem;
        private ErrorProvider errorProvider;
        private EditMenuStrip editMenuStrip;
        private RadioButton rbObservableBindingList;
        private ToolStripButton btnChangeInner;
        private GroupBox groupBox2;
        private RadioButton rbModel;
        private RadioButton rbValidating;
        private RadioButton rbEditable;
        private RadioButton rbUndoable;
        private RadioButton rbObservableObject;
        private RadioButton rbObject;
        private GroupBox groupBox3;
        private RadioButton rbInnerObservableBindingList;
        private RadioButton rbInnerObservableCollection;
        private RadioButton rbInnerSortableBindingList;
        private RadioButton rbInnerList;
        private RadioButton rbObservableCollection;
        private RadioButton rbNoInnerList;
        private ToolStripButton btnSetProp;
        private RadioButton rbSortableBindingListSortOnChange;
        private RadioButton rbInnerBindingList;
        private GroupBox gbBoundToList;
        private Label label1;
        private GroupBox gbBoundToCurrentItem;
        private TextBox tbStringPropCurrent;
        private Label label3;
        private TextBox tbIntPropCurrent;
        private Label label4;
        private TextBox tbStringPropList;
        private Label label2;
        private TextBox tbIntPropList;
        private ToolTip toolTip;
        private ToolStripButton btnReset;
        private GroupBox gbGrid;
        private GroupBox gbListBox;
        private ListBox listBox;
        private SafeDataGridView grid;
        private BindingSource listBindingSource;
        private BindingSource itemBindingSource;
        private DataGridViewTextBoxColumn intPropDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn stringPropDataGridViewTextBoxColumn;
        private ErrorProvider warningProvider;
        private ErrorProvider infoProvider;
        private Components.ValidationResultToErrorProviderAdapter warningAdapter;
        private Components.ValidationResultToErrorProviderAdapter infoAdapter;
        private Label lblInfo;
    }
}