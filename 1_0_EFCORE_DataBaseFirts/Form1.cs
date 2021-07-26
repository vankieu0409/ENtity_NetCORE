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
        private List<AccountsAdo> _lstAccounts;
        private DatabaseContext aContext;
        public Form1()
        {
            aContext = new DatabaseContext();
            InitializeComponent();
            LoadDaTa();
        }

        void LoadDaTa()
        {
            _lstAccounts = new List<AccountsAdo>(); // khởi tạo lại
            _lstAccounts = aContext.AccountsAdos.ToList(); // đổ dữ liệu vài lis Hiện tại
            //dếm thuộc tính có trong đối tượng
            Type type = typeof(AccountsAdo);
            int SLthuoctinh = type.GetProperties().Length;
            dataGridView1.ColumnCount = SLthuoctinh + 1;
            dataGridView1.Columns[0].Name = " Tài khoản";
            dataGridView1.Columns[1].Name = " Mật Khẩu";
            dataGridView1.Columns[2].Name = " Giới tính";
            dataGridView1.Columns[3].Name = " Năm Sinh";
            dataGridView1.Columns[4].Name = " Trạng thái";
            dataGridView1.Columns[5].Name = " Tuổi";
            dataGridView1.Columns[6].Name = " ID";
            dataGridView1.Rows.Clear();
            foreach (var x in _lstAccounts)
            {
                dataGridView1.Rows.Add(x.Acc, x.Pass, x.Sex == 1 ? "Nam" : x.Sex == 0 ? "Nữ" : "",
                    x.YearofBirth, x.Status == true ? "Hoạt Động" : "Không Hoạt Động",
                    DateTime.Now.Year - x.YearofBirth, x.Id);
            }
        }
    }
}
