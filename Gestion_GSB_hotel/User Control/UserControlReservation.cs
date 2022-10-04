using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Gestion_GSB_hotel.User_Control
{
    public partial class UserControlReservation : UserControl
    {
        DbConnector db;
        private string RID = "", No = "";
        public UserControlReservation()
        {
            InitializeComponent();
            db = new DbConnector();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private class DbConnector
        {
            private bool check = false;

            private SqlConnection GetConnection()
            {
                string connectionString = "Data Source = DESKTOP-FARHMED;\r\n                           Initial Catalog = Hotel_Management_System;\r\n                           Integrated Security = true";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                try
                {
                    sqlConnection.Open();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error! \n" + ex.ToString(), "SQL connection", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

                return sqlConnection;
            }

            public bool IsValidNamePass(string Username, string Password)
            {
                try
                {
                    string cmdText = "SELECT User_Name, User_Password FROM User_Table WHERE User_Name = '" + Username + "' AND User_Password = '" + Password + "'";
                    SqlConnection connection = GetConnection();
                    SqlCommand selectCommand = new SqlCommand(cmdText, connection);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    connection.Close();
                    if (dataTable.Rows.Count > 0)
                    {
                        check = true;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error! \n" + ex.ToString(), "Username and Password", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

                return check;
            }

            public bool AddUser(string Username, string Password)
            {
                string cmdText = "INSERT INTO User_Table VALUES (@User_Name, @User_Password)";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@User_Name", SqlDbType.VarChar).Value = Username;
                sqlCommand.Parameters.Add("@User_Password", SqlDbType.VarChar).Value = Password;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Added Successfully!", "User Added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Username already exist.", "Username", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Error! \n" + ex.ToString(), "Add User", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                    return false;
                }

                connection.Close();
                return true;
            }

            public bool UpdateUser(string ID, string Username, string Password)
            {
                string cmdText = "UPDATE User_Table SET User_Name = @UserName, User_Password = @UserPassword WHERE User_ID = @UserID";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = ID;
                sqlCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = Username;
                sqlCommand.Parameters.Add("@UserPassword", SqlDbType.VarChar).Value = Password;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully!", "User Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Username already exist.", "Username", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Error! \n" + ex.ToString(), "Update User", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                    return false;
                }

                connection.Close();
                return true;
            }

            public bool DeleteUser(string ID)
            {
                string cmdText = "DELETE FROM User_Table WHERE User_ID = @UserID";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = ID;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfully!", "User Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error! \n" + ex.ToString(), "Delete User", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                connection.Close();
                return true;
            }

            public void DisplayAndSearch(string query, DataGridView dgv)
            {
                SqlConnection connection = GetConnection();
                SqlCommand selectCommand = new SqlCommand(query, connection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                dgv.DataSource = dataTable;
            }

            public bool AddClient(string FirstName, string LastName, string Phone, string Address)
            {
                string cmdText = "INSERT INTO Client_Table VALUES (@Client_FirstName, @Client_LastName, @Client_Phone, @Client_Address)";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@Client_FirstName", SqlDbType.VarChar).Value = FirstName;
                sqlCommand.Parameters.Add("@Client_LastName", SqlDbType.VarChar).Value = LastName;
                sqlCommand.Parameters.Add("@Client_Phone", SqlDbType.VarChar).Value = Phone;
                sqlCommand.Parameters.Add("@Client_Address", SqlDbType.VarChar).Value = Address;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Added Successfully!", "Client Added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Phone No. already exist.", "Phone No.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Error! \n" + ex.ToString(), "Add Client", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                    return false;
                }

                connection.Close();
                return true;
            }

            public bool UpdateClient(string ID, string FirstName, string LastName, string Phone, string Address)
            {
                string cmdText = "UPDATE Client_Table SET Client_FirstName = @ClientFirstName, Client_LastName = @ClientLastName, Client_Phone = @ClientPhone, Client_Address = @ClientAddress WHERE Client_ID = @ClientID";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@ClientID", SqlDbType.Int).Value = ID;
                sqlCommand.Parameters.Add("@ClientFirstName", SqlDbType.VarChar).Value = FirstName;
                sqlCommand.Parameters.Add("@ClientLastName", SqlDbType.VarChar).Value = LastName;
                sqlCommand.Parameters.Add("@ClientPhone", SqlDbType.VarChar).Value = Phone;
                sqlCommand.Parameters.Add("@ClientAddress", SqlDbType.VarChar).Value = Address;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully!", "Client Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Phone No. already exist.", "Phone No.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Error! \n" + ex.ToString(), "Update Client", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                    return false;
                }

                connection.Close();
                return true;
            }

            public bool DeleteClient(string ID)
            {
                string cmdText = "DELETE FROM Client_Table WHERE Client_ID = @ClientID";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@ClientID", SqlDbType.Int).Value = ID;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfully!", "Client Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error! \n" + ex.ToString(), "Delete Client", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                connection.Close();
                return true;
            }

            public bool AddRoom(string Type, string Phone, string Free)
            {
                string cmdText = "INSERT INTO Room_Table VALUES (@Type, @Phone, @Free)";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = Type;
                sqlCommand.Parameters.Add("@Phone", SqlDbType.VarChar).Value = Phone;
                sqlCommand.Parameters.Add("@Free", SqlDbType.VarChar).Value = Free;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Added Successfully!", "Room Added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Phone No. already exist.", "Phone No.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Error! \n" + ex.ToString(), "Add Room", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                    return false;
                }

                connection.Close();
                return true;
            }

            public bool UpdateRoom(string No, string Type, string Phone, string Free)
            {
                string cmdText = "UPDATE Room_Table SET Room_Type = @RoomType, Room_Phone = @RoomPhone, Room_Free = @RoomFree WHERE Room_Number = @RoomNumber";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@RoomNumber", SqlDbType.Int).Value = No;
                sqlCommand.Parameters.Add("@RoomType", SqlDbType.VarChar).Value = Type;
                sqlCommand.Parameters.Add("@RoomPhone", SqlDbType.VarChar).Value = Phone;
                sqlCommand.Parameters.Add("@RoomFree", SqlDbType.VarChar).Value = Free;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully!", "Room Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Phone No. already exist.", "Phone No.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Error! \n" + ex.ToString(), "Update Room", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                    return false;
                }

                connection.Close();
                return true;
            }

            public bool DeleteRoom(string No)
            {
                string cmdText = "DELETE FROM Room_Table WHERE Room_Number = @RoomNumber";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@RoomNumber", SqlDbType.Int).Value = No;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfully!", "Room Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error! \n" + ex.ToString(), "Delete Room", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                connection.Close();
                return true;
            }

            public void RoomTypeAndNo(string query, ComboBox cb)
            {
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                cb.Items.Clear();
                cb.Items.Add("Please select ...");
                cb.SelectedIndex = 0;
                while (sqlDataReader.Read())
                {
                    cb.Items.Add(sqlDataReader[0]);
                }
            }

            public void UpdateReservationRoom(string No, string Free)
            {
                string cmdText = "UPDATE Room_Table SET Room_Free = @RoomFree WHERE Room_Number = @RoomNumber";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@RoomNumber", SqlDbType.Int).Value = No;
                sqlCommand.Parameters.Add("@RoomFree", SqlDbType.VarChar).Value = Free;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error! \n" + ex.ToString(), "Update Reservation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

                connection.Close();
            }

            public bool AddReservation(string Type, string No, string CID, string In, string Out)
            {
                string cmdText = "INSERT INTO Reservation_Table VALUES (@Type, @No, @CID, @In, @Out)";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = Type;
                sqlCommand.Parameters.Add("@No", SqlDbType.Int).Value = No;
                sqlCommand.Parameters.Add("@CID", SqlDbType.Int).Value = CID;
                sqlCommand.Parameters.Add("@In", SqlDbType.VarChar).Value = In;
                sqlCommand.Parameters.Add("@Out", SqlDbType.VarChar).Value = Out;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Added Successfully!", "Reservation Added", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Client ID already exist.", "Client ID", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Error! \n" + ex.ToString(), "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                    return false;
                }

                connection.Close();
                return true;
            }

            public bool UpdateReservation(string RID, string Type, string No, string CID, string In, string Out)
            {
                string cmdText = "UPDATE Reservation_Table SET Reservation_Room_Type = @ReservationRoomType, Reservation_Room_Number = @ReservationRoomNumber, Reservation_Client_ID = @ReservationClientID, Reservation_In = @ReservationIn, Reservation_Out = @ReservationOut WHERE Reservation_ID = @ReservationID";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@ReservationID", SqlDbType.Int).Value = RID;
                sqlCommand.Parameters.Add("@ReservationRoomType", SqlDbType.VarChar).Value = Type;
                sqlCommand.Parameters.Add("@ReservationRoomNumber", SqlDbType.Int).Value = No;
                sqlCommand.Parameters.Add("@ReservationClientID", SqlDbType.Int).Value = CID;
                sqlCommand.Parameters.Add("@ReservationIn", SqlDbType.VarChar).Value = In;
                sqlCommand.Parameters.Add("@ReservationOut", SqlDbType.VarChar).Value = Out;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully!", "Reservation Updated", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Client ID already exist.", "Client ID", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Error! \n" + ex.ToString(), "Update Reservation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                    return false;
                }

                connection.Close();
                return true;
            }

            public bool DeleteReservation(string ID)
            {
                string cmdText = "DELETE FROM Reservation_Table WHERE Reservation_ID = @ReservationID";
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.Add("@ReservationID", SqlDbType.Int).Value = ID;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Canceled Successfully!", "Reservation Canceled", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error! \n" + ex.ToString(), "Canceled Reservation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }

                connection.Close();
                return true;
            }

            public int Count(string query)
            {
                SqlConnection connection = GetConnection();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                return (int)sqlCommand.ExecuteScalar();
            }
        }

        public void Clear ()
        {
            comboBoxType.SelectedIndex = 0;
            comboBoxNo.SelectedIndex = 0;
            textBoxClientID.Clear();
            dateTimePickerIn.Value = DateTime.Now;
            dateTimePickerOut.Value = DateTime.Now;
            tabControlReservation.SelectedTab = tabPageAddReservation;
        }

        private void UserControlReservation_Load(object sender, EventArgs e)
        {
            comboBoxType.SelectedIndex = 0;
            comboBoxNo.SelectedIndex = 0;
            comboBoxType1.SelectedIndex = 0; 
            comboBoxNo1.SelectedIndex = 0;
        }

        private void buttonAdd_Leave(object sender, EventArgs e)
        {
           
        }

        private void tabPageAddReservation_Leave(object sender, EventArgs e)
        {
            Clear();
            Clear1();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            bool check;
            if (comboBoxType.SelectedIndex == 0 || comboBoxNo.SelectedIndex == 0 || textBoxClientID.Text.Trim() == string.Empty  )
                MessageBox.Show("Please fill out all fields.", "Require all field.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                check = db.AddReservation(comboBoxType.SelectedItem.ToString(), comboBoxNo.SelectedItem.ToString(),textBoxClientID.Text.Trim(), dateTimePickerIn.Text, dateTimePickerOut.Text);
                db.UpdateReservationRoom(comboBoxNo.SelectedItem.ToString(), "No");
                if (check)
                    Clear();
            }
        }

        private void tabPageSearchReservation_Enter(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Reservation_Table", dataGridViewReservation);
        }

        private void tabPageSearchReservation_Leave(object sender, EventArgs e)
        {
            textBoxSearchClientID.Clear();
        }

        private void textBoxSearchClientID_TextChanged(object sender, EventArgs e)
        {
            db.DisplayAndSearch("SELECT * FROM Reservation_Table WHERE Reservation_Client_ID LIKE '%" + textBoxSearchClientID.Text + "%'", dataGridViewReservation);
        }

        private void dataGridViewReservation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewReservation.Rows[e.RowIndex];
                RID = row.Cells[0].Value.ToString();
                comboBoxType1.SelectedItem = row.Cells[1].Value.ToString();
                comboBoxNo1.SelectedItem = No = row.Cells[2].Value.ToString();
                textBoxClientID1.Text = row.Cells[3].Value.ToString();
                dateTimePickerIn1.Text = row.Cells[4].Value.ToString();
                dateTimePickerOut1.Text = row.Cells[5].Value.ToString();

            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            bool check;
            if (RID != "")
            {
                if (comboBoxType1.SelectedIndex == 0 || comboBoxNo1.SelectedIndex == 0 || textBoxClientID1.Text.Trim() == string.Empty)
                    MessageBox.Show("Please fill out all fields.", "Require all field.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    check = db.UpdateReservation(RID, comboBoxType1.SelectedItem.ToString(), comboBoxNo1.SelectedItem.ToString(), textBoxClientID1.Text.Trim(), dateTimePickerIn1.Text, dateTimePickerOut1.Text);
                    db.UpdateReservationRoom(comboBoxNo1.SelectedItem.ToString(), "No");
                    db.UpdateReservationRoom(No, "Yes");
                    if (check)
                        Clear1();
                }
            }
            else
            {
                MessageBox.Show("Veuillez d'abord sélectionner la ligne du tableau.", "Tout les champs requis.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            bool check;
            if (RID != "")
            {
                if (comboBoxType1.SelectedIndex == 0 || comboBoxNo1.SelectedIndex == 0 || textBoxClientID1.Text.Trim() == string.Empty)
                    MessageBox.Show("Please fill out all fields.", "Require all field.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    DialogResult result = MessageBox.Show("Êtes vous sûre de vouloir annuler cette réservation?", "Supprimer réservation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == result)
                    {
                        check = db.DeleteReservation(RID);
                        db.UpdateReservationRoom(No, "Yes");
                        if (check)
                            Clear1();
                    }

                }
            }
        }

        private void tabPageUpdateandCancelReservation_Leave(object sender, EventArgs e)
        {
            Clear1();
        }

        private void comboBoxType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.RoomTypeAndNo("SELECT Room_Number FROM Room_Table WHERE Room_Type='" + comboBoxType1.SelectedItem.ToString() + "'AND Room_Free = 'Yes' ORDER BY Room_Number", comboBoxNo1);
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            db.RoomTypeAndNo("SELECT Room_Number FROM Room_Table WHERE Room_Type='" + comboBoxType.SelectedItem.ToString() + "'AND Room_Free = 'Yes' ORDER BY Room_Number", comboBoxNo);
        }

        public void Clear1()
        {
            comboBoxType1.SelectedIndex = 0;
            comboBoxNo1.SelectedIndex = 0;
            textBoxClientID1.Clear();
            dateTimePickerIn1.Value = DateTime.Now;
            dateTimePickerOut1.Value = DateTime.Now;
            RID = "";
            
        }


    }
}
