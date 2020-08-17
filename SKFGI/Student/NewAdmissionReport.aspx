﻿<%@ Page Language="C#" MasterPageFile="~/MasterAdmin.Master" AutoEventWireup="true"
    CodeBehind="NewAdmissionReport.aspx.cs" Inherits="CollegeERP.Student.NewAdmissionReport"
    Title="New Admission Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="Sc1" runat="server">
    </asp:ScriptManager>
    <div class="title">
        <h5>
            New Admission Report</h5>
    </div>
    <%--<asp:UpdatePanel ID="UP1" runat="server">
        <ContentTemplate>--%>
            <div style="width: 1180px;">
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="left" width="10%" class="label">
                            Course
                        </td>
                        <td align="left" width="10%" class="label">
                            Stream
                        </td>
                        <td align="left" width="10%" class="label">
                            Batch
                        </td>
                        <td align="left" width="10%" class="label">
                            State
                        </td>
                        <td align="left" width="10%" class="label">
                            District
                        </td>
                        <td align="left" width="20%" class="label">
                            City
                        </td>
                        <td align="left" width="20%" class="label">
                            <%--School/College--%>
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="10%">
                            <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" CssClass="dropdownList"
                                Width="140px" DataValueField="CourseId" DataTextField="CourseName" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="10%">
                            <asp:DropDownList ID="ddlStream" runat="server" CssClass="dropdownList" Width="140px"
                                DataTextField="stream_name" DataValueField="StreamId">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="10%">
                            <asp:DropDownList ID="ddlBatch" runat="server" CssClass="dropdownList" Width="140px"
                                DataValueField="id" DataTextField="batch_name">
                            </asp:DropDownList>
                        </td>
                        <td align="left" width="10%">
                                            
                    <asp:DropDownList ID="ddlstate" runat="server" CssClass="dropdownList" 
                        Width="140px" AutoPostBack="True" 
                        onselectedindexchanged="ddlstate_SelectedIndexChanged">
                    </asp:DropDownList>
                
                        </td>



                         <td align="left" width="10%">
                                            
                                       
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdownList" 
                        Width="140px" AutoPostBack="True" 
                        onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                    </asp:DropDownList>
                
                
                        </td>
                         <td align="left" width="20%">
                                            
                                       
                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="dropdownList" 
                        Width="140px"  
                      >
                    </asp:DropDownList>
                
                
                        </td>
                         <td align="left" width="20%">
                                            
                                          
                        <%--<asp:DropDownList ID="ddlSchool" runat="server" CssClass="dropdownList" 
                         Width="140px">
                    </asp:DropDownList>--%>
                    
                
                        </td>


                        <td align="left">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" 
                                onclick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <table width="100%" align="center" class="table">
                    <tr>
                        <td align="center">
                            <asp:Panel ID="PNLGrid" runat="server" ScrollBars="Both" Width="980px" Height="350px">
                                <asp:GridView ID="dgvStudent" runat="server" AllowPaging="false" Width="100%" GridLines="None">
                                    <HeaderStyle CssClass="HeaderStyle" />
                                    <RowStyle CssClass="RowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnDownload" runat="server" CssClass="button" Text="Download" 
                                onclick="btnDownload_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
