﻿using DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientDesktopApp
{
    public partial class FormNewObject : Form
    {
        public TestObject model;

        public FormNewObject(TestObject Model = null)
        {
            if(Model != null)
            {
                this.model = Model;
                this.Text = "Редактирование";
                textBoxName.Text = model.Name;
                textBoxDescription.Text = model.Description;
                textBoxTags.Text = string.Join(", ", model.Tags);
            }
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            List<string> tags = textBoxTags.Text.Replace(" ", "").Split(',').ToList();
            if(string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Error: name is empty");
                return;
            }
            
            if (model == null)
            {
                model = new TestObject(-1, textBoxName.Text, textBoxDescription.Text, tags);
            }
            else
            {
                model.Name = textBoxName.Text;
                model.Description = textBoxDescription.Text;
                model.Tags = tags;
            }
            this.DialogResult|= DialogResult.OK;
            this.Close();
        }
    }
}
