﻿using framework_crud.ORM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ORM_DEMO.Models;
using System.Diagnostics;

namespace ORM_DEMO
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            prepareData();
        }

        public FormMain(string tableName, string connstring)
        {
            this.tableName = tableName;
            this.connstring = connstring;
        }

        private string tableName = "ORM_DEMO.Models.Person";

        private string connstring = "Data Source=DESKTOP-15SIF8Q\\SQLEXPRESS; database=oop;" +
            "Integrated Security=True;Connect Timeout=10";
        private IList listData;
        private List<Models.students> students;
        private MSSQLDatabase database;
        private BindingSource bd = new BindingSource();

        private void prepareData()
        {
            lbNameTable.Text = tableName;
            // connect database
            database = new MSSQLDatabase(new System.Data.SqlClient.SqlConnection(connstring));

            // load data
            loadData();

            grvData.Dock = DockStyle.Fill;
            grvData.AutoGenerateColumns = true;
            this.Refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Show add form of table
            Views.students frmAdd = new Views.students();
            frmAdd.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            students = new List<Models.students>();
            listData = database.Table(typeof(Models.students)).Query().Select();

            foreach (Models.students student in listData)
            {
                students.Add(student);
            }

            // Set up the DataGridView.
            grvData.Dock = DockStyle.Fill;

            // Automatically generate the DataGridView columns.
            grvData.AutoGenerateColumns = true;
            
            bd.DataSource = students;
            grvData.DataSource = bd;

            grvData.Update();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Models.students selectedRow = (Models.students) grvData.CurrentRow.DataBoundItem;
            database.Table(typeof(Models.students)).Delete(selectedRow);
            MessageBox.Show("Delete successfully. Refresh...");
            loadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grvData.CurrentRow == null) return;
            Models.students selectedRow = (Models.students)grvData.CurrentRow.DataBoundItem;
            Views.students updateForm = new Views.students(selectedRow);
            updateForm.Show();
        }
    }
}
