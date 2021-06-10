using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Admin
{
    public partial class Form1 : Form
    {

        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataAdapter adpt;
        private String connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\C#\Database1\Doctor.accdb";

        private String fname;
        private String lname;
        private String speciality;
        private String address;
        private String empType;
        private String NIC;
        private int id;
        private int SLMC;
        private int tp;
        private String b;
        private static int empno = empno + n;
        private static int n = 2;

        public Form1()
        { 
            conn = new SqlConnection(connString);
            InitializeComponent();
        }
        

        //panel Control

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void btnNewDocRec_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            btnNewDocRec.BorderStyle = BorderStyle.Fixed3D;
            btnNewEmpRec.BorderStyle = BorderStyle.None;
            btnUpdateRecords.BorderStyle = BorderStyle.None;
            btnDeleteRecords.BorderStyle = BorderStyle.None;
        }

        private void btnNewEmpRec_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;

            btnNewDocRec.BorderStyle = BorderStyle.None;
            btnNewEmpRec.BorderStyle = BorderStyle.Fixed3D;
            btnUpdateRecords.BorderStyle = BorderStyle.None;
            btnDeleteRecords.BorderStyle = BorderStyle.None;
        }

        private void btnUpdateRecords_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;
            panel4.Visible = false;

            btnNewEmpRec.BorderStyle = BorderStyle.None;
            btnNewDocRec.BorderStyle = BorderStyle.None;
            btnUpdateRecords.BorderStyle = BorderStyle.Fixed3D;
            btnDeleteRecords.BorderStyle = BorderStyle.None;

            try
            {
                radUpDoctor.Checked = true;

                String query = "select * from Doctor";

                adpt = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                conn.Open();
                adpt.Fill(dt);
                conn.Close();

                dataGridView2.DataSource = dt;
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDeleteRecords_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;

            btnNewEmpRec.BorderStyle = BorderStyle.None;
            btnNewDocRec.BorderStyle = BorderStyle.None;
            btnUpdateRecords.BorderStyle = BorderStyle.None;
            btnDeleteRecords.BorderStyle = BorderStyle.Fixed3D;

            try
            {
                radDelDoc.Checked = true;

                String query = "select * from Doctor";

                adpt = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                conn.Open();
                adpt.Fill(dt);
                conn.Close();

                dataGridView1.DataSource = dt;
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


//insert

        private void btnInsertDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
                {
                    if (comboBox1.SelectedIndex >= 0)
                    {
                        if (textBox1.Text.All(char.IsLetter) && textBox2.Text.All(char.IsLetter))
                        {
                            if (textBox4.Text.All(char.IsDigit) && textBox6.Text.All(char.IsDigit))
                            {
                                if (textBox3.TextLength == 10 && textBox6.TextLength == 10)
                                {
                                    fname = textBox1.Text;
                                    lname = textBox2.Text;
                                    NIC = textBox3.Text;
                                    speciality = Convert.ToString(comboBox1.SelectedItem);
                                    SLMC = Convert.ToInt32(textBox4.Text);
                                    address = textBox5.Text;
                                    tp = Convert.ToInt32(textBox6.Text);

                                    String query = "insert into Doctor values (" + SLMC + ",'" + fname + "','" + lname + "','" + NIC + "','" + speciality + "','" + address + "'," + tp + ") ";

                                    cmd = new SqlCommand(query, conn);
                                    conn.Open();
                                    cmd.ExecuteNonQuery();
                                    conn.Close();

                                    MessageBox.Show("Record Added", "New Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    textBox1.Text = "";
                                    textBox2.Text = "";
                                    textBox3.Text = "";
                                    comboBox1.Text = "";
                                    textBox4.Text = "";
                                    textBox5.Text = "";
                                    textBox6.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("NIC/Telephone number fields must contain 10 characters", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("SLMC/Telephone number fields must contain Digits only", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("First Name/Last Name fields must contain characters only", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Speciality must be selected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No field must should be left empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnInsertEmp_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox7.Text != "" && textBox8.Text != "" && textBox10.Text != "" && textBox11.Text != "" && textBox12.Text != "")
                {
                    if (radNurse.Checked == true || radTester.Checked == true || radRec.Checked == true)
                    {
                        if (textBox12.Text.All(char.IsLetter) && textBox11.Text.All(char.IsLetter))
                        {
                            if (textBox10.TextLength == 10 && textBox7.TextLength == 10)
                            {
                                if (textBox7.Text.All(char.IsDigit))
                                {
                                    fname = textBox12.Text;
                                    lname = textBox11.Text;
                                    NIC = textBox10.Text;
                                    address = textBox8.Text;
                                    tp = Convert.ToInt32(textBox7.Text);

                                    if (radNurse.Checked)
                                    {
                                        empType = "nurse";
                                    }

                                    if (radTester.Checked)
                                    {
                                        empType = "lab tester";
                                    }

                                    if (radRec.Checked)
                                    {
                                        empType = "receptionist";
                                    }

                                    String query = "insert into Employee values ('" + fname + "','" + lname + "','" + NIC + "','" + empType + "','" + address + "'," + tp + ") ";

                                    cmd = new SqlCommand(query, conn);
                                    conn.Open();
                                    cmd.ExecuteNonQuery();
                                    conn.Close();

                                    MessageBox.Show("Record Added", "New Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    textBox12.Text = "";
                                    textBox11.Text = "";
                                    textBox10.Text = "";
                                    textBox8.Text = "";
                                    textBox7.Text = "";
                                    radNurse.Checked = true;
                                }
                                else
                                {
                                    MessageBox.Show("Telephone number fields must contain only Digits", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("NIC/Telephone number fields must contain 10 characters", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("First Name/Last Name fields must contain characters only", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee type must be selected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Any field should not be left empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //update

        private void radUpDoctor_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                String query = "select * from Doctor";

                adpt = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                conn.Open();
                adpt.Fill(dt);
                conn.Close();

                dataGridView2.DataSource = dt;
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radUpEmp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                String query = "select * from Employee";

                adpt = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                conn.Open();
                adpt.Fill(dt);
                conn.Close();

                dataGridView2.DataSource = dt;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            //it checks if the row index of the cell is greater than or equal to zero
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                textBox9.Text = row.Cells[5].Value.ToString();
                textBox13.Text = row.Cells[6].Value.ToString();
                id = Convert.ToInt32(row.Cells[0].Value.ToString());

            }
        }


        private void btnUp_Click(object sender, EventArgs e)
        {
               
                if (radUpDoctor.Checked)
                {
                   try
                   {
                    if (textBox9.Text != "" && textBox13.Text != "")
                    {
                        if (textBox13.Text.All(char.IsDigit))
                        {
                            if (textBox13.TextLength == 10)
                            {
                                String query = "UPDATE Doctor SET Address =' " + textBox9.Text + "', TP_number=" + Convert.ToInt32(textBox13.Text) + " WHERE SLMC=" + id;

                                cmd = new SqlCommand(query, conn);
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();

                                String query1 = "select * from Doctor";

                                adpt = new SqlDataAdapter(query1, conn);
                                DataTable dt = new DataTable();

                                conn.Open();
                                adpt.Fill(dt);
                                conn.Close();

                                dataGridView2.DataSource = dt;


                                MessageBox.Show("Record Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Telephone number field must contain 10 characters", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Telephone number fields must contain only digits", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Any field should not be empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                   }
                            
                   catch(Exception ex)
                   {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
                }


                else if (radUpEmp.Checked)
                {
                 try
                 {
                    if (textBox9.Text != "" && textBox13.Text != "")
                    {
                        if (textBox13.Text.All(char.IsDigit))
                        {
                            if (textBox13.TextLength == 10)
                            {
                                String query = "UPDATE Employee SET Address =' " + textBox9.Text + "', TP_number=" + Convert.ToInt32(textBox13.Text) + " WHERE ID=" + id;

                                cmd = new SqlCommand(query, conn);
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();

                                String query1 = "select * from Employee";

                                adpt = new SqlDataAdapter(query1, conn);
                                DataTable dt = new DataTable();

                                conn.Open();
                                adpt.Fill(dt);
                                conn.Close();

                                dataGridView2.DataSource = dt;

                                MessageBox.Show("Record Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Telephone number field must contain 10 characters", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Telephone number fields must contain only digits", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Any field should not be empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                 catch (Exception ex)
                 {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }

            }
            
        }


//delete

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //it checks if the row index of the cell is greater than or equal to zero
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                id = Convert.ToInt32(row.Cells[0].Value.ToString());
                b = row.Cells[3].Value.ToString();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (radDelDoc.Checked)
            {
                try
                {
                    String query = "DELETE * FROM Doctor WHERE SLMC=" + id;

                    cmd = new SqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    String query1 = "select * from Doctor";

                    adpt = new SqlDataAdapter(query1, conn);
                    DataTable dt = new DataTable();

                    conn.Open();
                    adpt.Fill(dt);
                    conn.Close();

                    dataGridView1.DataSource = dt;

                    MessageBox.Show("Record Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            else if (radDelEmp.Checked)
            {
                try
                {
                    String query = "DELETE * FROM Employee WHERE ID = " + id;

                    cmd = new SqlCommand(query, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    String query1 = "select * from Employee";

                    adpt = new SqlDataAdapter(query1, conn);
                    DataTable dt = new DataTable();

                    conn.Open();
                    adpt.Fill(dt);
                    conn.Close();

                    dataGridView1.DataSource = dt;

                    MessageBox.Show("Record Deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void radDelDoc_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                String query = "select * from Doctor";

                adpt = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                conn.Open();
                adpt.Fill(dt);
                conn.Close();

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radDelEmp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                String query = "select * from Employee";

                adpt = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                conn.Open();
                adpt.Fill(dt);
                conn.Close();

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }

