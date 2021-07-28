using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using _1_0_EFCORE_DataBaseFirts.Context;
using _1_0_EFCORE_DataBaseFirts.Models;

namespace _1_0_EFCORE_DataBaseFirts
{
    public partial class Form1 : Form
    {
        private AccountService _SVACC;
        private List<AccountsAdo> _lstAccounts;
        private DatabaseContext aContext;
        private AccountsAdo acc;
        public Form1()
        {
            _SVACC = new AccountService();
            aContext = new DatabaseContext();
            InitializeComponent();
            LoadDaTa();
            loadNamSinh();
            rbtn_Nam.Checked = true;
            cbx_.Checked = true;
        }
        void loadNamSinh()
        {
            foreach (var VARIABLE in _SVACC.getYearofBirth())
            {
                cbx_namsinh.Items.Add(VARIABLE);
            }
        }
        void LoadDaTa()
        {
            _lstAccounts = new List<AccountsAdo>(); // khởi tạo lại
            _lstAccounts = aContext.AccountsAdos.ToList(); // đổ dữ liệu vài lis Hiện tại
            //dếm thuộc tính có trong đối tượng
            Type type = typeof(AccountsAdo);
            int SLthuoctinh = type.GetProperties().Length;
            dataGridView1.ColumnCount = SLthuoctinh;
            dataGridView1.Columns[0].Name = " Tài khoản";
            dataGridView1.Columns[1].Name = " Mật Khẩu";
            dataGridView1.Columns[2].Name = " Giới tính";
            dataGridView1.Columns[3].Name = " Năm Sinh";
            dataGridView1.Columns[4].Name = " Trạng thái";
            dataGridView1.Columns[5].Name = " ID";
            dataGridView1.Rows.Clear();
            foreach (var x in _lstAccounts)
            {
                dataGridView1.Rows.Add(x.Acc, x.Pass, x.Sex == 1 ? "Nam" : x.Sex == 0 ? "Nữ" : "",
                    x.YearofBirth, x.Status == true ? "Hoạt Động" : "Không Hoạt Động", x.Id);
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            acc = new AccountsAdo();
            acc.Acc = tbx_tk.Text;
            acc.Pass = tbx_mk.Text;
            acc.Sex = rbtn_Nam.Checked ? 1 : 0;
            acc.YearofBirth = Convert.ToInt16(cbx_namsinh.Text);
            acc.Status = cbx_.Checked ? true : false;
            _SVACC.AddnewACC(acc);
            MessageBox.Show(" Thêm tài Khoản thành Công", "Thông báo Của UBND xã TUÂN CHÍNH");
            LoadDaTa();


        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            acc = new AccountsAdo();
            acc.Acc = tbx_tk.Text;
            acc.Pass = tbx_mk.Text;
            acc.Sex = rbtn_Nam.Checked ? 1 : 0;
            acc.YearofBirth = Convert.ToInt16(cbx_namsinh.Text);
            acc.Status = cbx_.Checked ? true : false;
            _SVACC.UpdateAcc(acc);
            MessageBox.Show(" Sửa tài Khoản thành Công!", "Thông báo Của UBND xã TUÂN CHÍNH");
            LoadDaTa();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            _lstAccounts = new List<AccountsAdo>();
            _lstAccounts = _SVACC.getListACC_Service();
            var index = _lstAccounts.FindIndex(c => c.Acc == tbx_tk.Text);
            _SVACC.DeleteACC(_lstAccounts[index].Id);
            MessageBox.Show(" Xóa tài Khoản thành Công!", "Thông báo Của UBND xã TUÂN CHÍNH");
            LoadDaTa();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //1. Lấy Index Rows Khi bấm vào Gird
            int rowIndex = e.RowIndex;
            if (rowIndex == _lstAccounts.Count) return;
            // 2. Lấy giá trị tại cột ID
            //  idAccWhenClick = Convert.ToInt16(gv_data.Rows[rowIndex].Cells[6].Value);
            // 3. có 2 cách để tìm được dối tượng
            // 3.1- Dùng Service dể tìm
            // 3.2 là sử dụng cách lấy giá trị tại cột
            tbx_tk.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            tbx_mk.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            rbtn_Nam.Checked = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString() == "Nam" ? true : false;
            rbtn_Nu.Checked = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString() == "Nữ" ? true : false;
            cbx_namsinh.SelectedIndex = cbx_namsinh.FindString(dataGridView1.Rows[rowIndex].Cells[3].Value.ToString());
            cbx_.Checked = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString() == "Hoạt Động" ? true : false;
            cbx_Khd.Checked = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString() == "Không Hoạt Động" ? true : false;
        }
        private void cbx__CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_.Checked)
            {
                cbx_Khd.Checked = false;
            }
        }

        private void cbx_Khd_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Khd.Checked)
            {
                cbx_.Checked = false;
            }
        }

        private void textBox1_KeyUp(object sender, EventArgs e)
        {
            _lstAccounts = new List<AccountsAdo>();

            _lstAccounts=_SVACC.GetListACCByStartWith(textBox1.Text);
            Type type = typeof(AccountsAdo);
            int SLthuoctinh = type.GetProperties().Length;
            dataGridView1.ColumnCount = SLthuoctinh;
            dataGridView1.Columns[0].Name = " Tài khoản";
            dataGridView1.Columns[1].Name = " Mật Khẩu";
            dataGridView1.Columns[2].Name = " Giới tính";
            dataGridView1.Columns[3].Name = " Năm Sinh";
            dataGridView1.Columns[4].Name = " Trạng thái";
            dataGridView1.Columns[5].Name = " ID";
            dataGridView1.Rows.Clear();
            foreach (var x in _lstAccounts)
            {
                dataGridView1.Rows.Add(x.Acc, x.Pass, x.Sex == 1 ? "Nam" : x.Sex == 0 ? "Nữ" : "",
                    x.YearofBirth, x.Status == true ? "Hoạt Động" : "Không Hoạt Động", x.Id);
            }
        }
    }
}
