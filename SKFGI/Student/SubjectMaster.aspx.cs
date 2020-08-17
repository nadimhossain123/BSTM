using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;



namespace CollegeERP.Student
{
    public partial class SubjectMaster : System.Web.UI.Page
    {
        public int SubjectId
        {
            get { return Convert.ToInt32(ViewState["SubjectId"]); }
            set { ViewState["SubjectId"] = value; }
        }
        System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem("---SELECT---", "0");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCourse();
                LoadSubjectList();
                ClearControls();

            }
        }
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //}

        protected void LoadCourse()
        {
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 2;
            Registration.CourseId = 0; // Course Id is not required to fetch all courses
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            if (dt != null)
            {
                ddlCourse.DataSource = dt;
                ddlCourse.DataBind();
            }
            LoadStream();
        }

        protected void LoadStream()
        {
            int CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            BusinessLayer.Student.BTechRegistration ObjRegistration = new BusinessLayer.Student.BTechRegistration();
            Entity.Student.BTechRegistration Registration = new Entity.Student.BTechRegistration();
            Registration.intMode = 3;
            Registration.CourseId = CourseId;
            DataTable dt = ObjRegistration.GetAllCommonSP(Registration);
            if (dt != null)
            {
                ddlStream.DataSource = dt;
                ddlStream.DataBind();
            }
        }
        protected void LoadSubjectList()
        {
            BusinessLayer.student.Subject ObjSubject = new BusinessLayer.student.Subject();
            DataTable dt = ObjSubject.GetAll();
            if (dt != null)
            {
                dgvSubject.DataSource = dt;
                dgvSubject.DataBind();

                ddlParentSubject.DataSource = dt;
                ddlParentSubject.DataBind();
            }
            ddlParentSubject.Items.Insert(0, li);
        }

        protected void ClearControls()
        {
            SubjectId = 0;
            ddlCourse.SelectedIndex = 0;
            LoadStream();
            ddlStream.SelectedValue = "0";
            txtSubjectCode.Text = "";
            txtSubjectName.Text = "";
            ddlParentSubject.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlSubjectType.SelectedIndex = 0;
            txtMinAttendance.Text = "";
            Message.Show = false;
        }

        protected void LoadSubjectDetails()
        {
            BusinessLayer.student.Subject ObjSubject = new BusinessLayer.student.Subject();
            Entity.Student.Subject Subject = new Entity.Student.Subject();
            Subject = ObjSubject.GetAllById(SubjectId);
            if (Subject != null)
            {
                txtSubjectCode.Text = Subject.SubjectCode;
                txtSubjectName.Text = Subject.SubjectName;
                ddlCourse.SelectedValue = Subject.CourseId.ToString();
                LoadStream();
                ddlStream.SelectedValue = Subject.StreamId.ToString();
                ddlParentSubject.SelectedValue = Subject.ParentSubjectId_FK.ToString();
                ddlSemester.SelectedValue = Subject.SemNo.ToString();
                ddlSubjectType.SelectedValue = Subject.IsPractical ? "1" : "0";
                txtMinAttendance.Text = Convert.ToString(Subject.MinAttendence);
                Message.Show = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BusinessLayer.student.Subject ObjSubject = new BusinessLayer.student.Subject();
            Entity.Student.Subject Subject = new Entity.Student.Subject();
            Subject.SubjectId = SubjectId;
            Subject.SubjectCode = txtSubjectCode.Text.Trim();
            Subject.SubjectName = txtSubjectName.Text.Trim();
            Subject.ParentSubjectId_FK = int.Parse(ddlParentSubject.SelectedValue);
            Subject.CourseId = int.Parse(ddlCourse.SelectedValue.Trim());
            Subject.StreamId = int.Parse(ddlStream.SelectedValue.Trim());
            Subject.SemNo = int.Parse(ddlSemester.SelectedValue);
            Subject.IsPractical = (ddlSubjectType.SelectedValue == "1") ? true : false;
            Subject.MinAttendence = int.Parse(txtMinAttendance.Text);

            int RowsAffected = ObjSubject.Save(Subject);
            if (RowsAffected != -1)
            {
                ClearControls();
                LoadSubjectList();
                Message.IsSuccess = true;
                Message.Text = "Subject Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Save. Duplicate Subject Code Is Not Allowed";
            }
            Message.Show = true;
        }

        protected void dgvSubject_RowEditing(object sender, GridViewEditEventArgs e)
        {
            SubjectId = Convert.ToInt32(dgvSubject.DataKeys[e.NewEditIndex].Value);
            LoadSubjectDetails();
        }

        protected void dgvSubject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(dgvSubject.DataKeys[e.RowIndex].Value);
            BusinessLayer.student.Subject ObjSubject = new BusinessLayer.student.Subject();
            int RowsAffected = ObjSubject.Delete(Id);
            if (RowsAffected != -1)
            {
                LoadSubjectList();
                Message.Show = false;
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can Not Delete. One or More Teacher Is Attached With This Subject.";
                Message.Show = true;
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStream();
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            BusinessLayer.student.Subject ObjSubject = new BusinessLayer.student.Subject();
            DataTable dt = ObjSubject.GetAllForDownload();
            ExporttoExcel(dt);

        }
           
        
        private void ExporttoExcel(DataTable table)
            {
           
            DataTable dt = table;
            string attachment = "attachment; filename=Subject Details.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";

           
           foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
          
            Response.End();


        }

        }

}


