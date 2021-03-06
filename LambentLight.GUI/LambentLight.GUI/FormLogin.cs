using LambentLight.GUI.API;
using System;
using System.Windows.Forms;

namespace LambentLight.GUI
{
    public partial class FormLogin : Form
    {
        #region Constructor

        public FormLogin()
        {
            InitializeComponent();
        }

        #endregion

        #region Tools

        public void SetItemVisibility(bool enabled)
        {
            IPTextBox.Enabled = enabled;
            TokenTextBox.Enabled = enabled;
            RememberCheckBox.Enabled = enabled;
            AutoCheckBox.Enabled = enabled;
            ConnectButton.Enabled = enabled;
        }

        #endregion

        #region Events

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            // If the host field is empty, return
            if (string.IsNullOrWhiteSpace(IPTextBox.Text))
            {
                MessageBox.Show("The Host is empty or just whitespaces.", "Invalid Host", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Disable the items to prevent them from being pressed
            SetItemVisibility(false);
            // And check if the host is valid by calling /
            if (!await new Client(IPTextBox.Text, TokenTextBox.Text).IsValid())
            {
                MessageBox.Show($"The host refused to connect.", "Invalid Host", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetItemVisibility(true);
                return;
            }

            // If everything went well, close the form so the rest of the program can take over
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion
    }
}
