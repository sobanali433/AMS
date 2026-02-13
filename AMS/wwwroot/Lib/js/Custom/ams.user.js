var ams = ams || {};

ams.user = new function () {
    this.Option = {
        Table: null,
        TableId: "",
        SearchId: "",
        RoleId: 0,
        AttendanceTable: null,
    }
    this.Init = function (options) {
        ams.user.Option = $.extend({}, ams.user.Option, options);
        ams.user.Option.Table = $("#userTableId").DataTable(
            {
                paging: true,
                serverSide: true,
                processing: true,
                lengthChange: true,
                info: true,
                async: false,
                lengthMenu: [[20, 50, 100, 2564500], [20, 50, 100, 2564500]],
                pageLength: 20,
                dom: '<"top-toolbar"<"toolbar-top"if<"entries-info">><"export-btn-container"><"entries-dropdown"><"search-bar-container">>rt<"bottom-toolbar"<"toolbar-bottom"p>>',
                language: {
                    search: '<i class="ri-search-line" onclick="SnjCrm.Lead.Search()"></i>', 
                    searchPlaceholder: "Search...",
                    paginate: {
                        next: '<i class="ri-arrow-right-s-line"></i>',
                        previous: '<i class="ri-arrow-left-s-line"></i>'
                    }
                },
                ajax: {
                    type: "Post",
                    url: '/User/GetList',
                    data: function (dtParms) {
                        dtParms.search.value = $("#txtSearch").val();
                        return dtParms;
                    },
                    complete: function (response, result) { }
                },
                "columns": [
                    {
                        data: "userMasterId", name: "userMasterId", orderable: false, render: function (data, type, row) {

                            var renderResult = "", btnEdit = "";
                            //if (SNJAMS.User.Option.RoleId == SNJAMS.Common.Role.SuperAdmin || SNJAMS.User.Option.RoleId == SNSJAMS.Common.Role.HrManager || SNJAMS.User.Option.RoleId == SNJAMS.Common.Role.Finance || SNJAMS.User.Option.RoleId == SNJAMS.Common.Role.Recruiter) {
                            //renderResult += '<div class="form-check"><input type="checkbox" class="deleteAll mr-2 fs-0 form-check-input" value="' + data + '" onChange="SNJDC.User.OnSelectRecord()"/>';
                            renderResult += '<div class="form-check">';
                            renderResult += '&nbsp;<i class="fas fa-edit ml-2" style="cursor: pointer;" onclick="ams.user.Add(\'' + row.userMasterId + '\',)"></i>';
                            renderResult += '&nbsp;<i class="fas fa-trash-alt ml-2" style="cursor: pointer;" onclick="ams.user.Delete(\'' + row.userMasterId + '\',\'' + row.isActive + '\')"></i>';
                            //renderResult += '&nbsp;<a href="' + UrlContent("User/Detail/" + row.encryptUserMasterId) + '"><i class="fas fa-file ml-2" style="cursor: pointer;" ></i></a>';
                            renderResult += '</div>';

                            return renderResult;
                        }
                    },
                    { data: "firstName", name: "FirstName" },
                    { data: "lastName", name: "LastName" },
                    {  data: "username", name: "userName"},
                    { data: "branchName", name: "branchName"},
                    { data: "createdOn", name: "createdOn"},
                    {
                        data: "isActive", name: "isActive", className: "text-center col-1",
                        render: function (data, type, row) {
                            var badge = ''
                            if (row.isActive)
                                badge += '<span class="badge bg-success-subtle text-success">Active</span>'
                            else
                                badge += '<span class="badge bg-danger-subtle text-danger">In-Active</span>'
                            return badge;
                        }
                    },
                 
                
                    //{ data: "username", name: "Username", },

                    

                    //{ data: "firstName", name: "FirstName", },
                    //{ data: "lastName", name: "LastName" },

                    //{ data: "contactNumber", name: "ContactNumber" },
                  
                ],
                order: [[0, "ASC"]],
            });
    }
    this.Add = function (id = '') {
        ams.common.HandleLoadingButton("#addNewEmployeeBtnId", function (revert) {
            $.ajax({
                type: "GET",
                url: "/User/_Details?id=" + id,  
                success: function (data) {
                    $("#commonlargeModalContent").html(data);
                    ams.common.InitMask();
                    $.validator.unobtrusive.parse($("#AddUserform"));
                    $("#commonlargeModal").modal('show');
                    //HideLoader();
                    //Button Reverted From Loading
                    revert();   
                }
            });
        });
    };
    this.Save = function () {
        if ($("#AddUserform").valid()) {
            //ShowLoader();
            var formdata = $("#AddUserform").serialize();
            ams.common.HandleLoadingButton("#saveEmployeeButtonId", function (revert) {
                $.ajax({    
                    type: "Post",
                    url: "/User/Save/",
                    data: formdata,
                    success: function (result) {
                        //HideLoader();
                        if (result.isSuccess) {
                            ams.user.Option.Table.ajax.reload();
                            ams.common.ToastrSuccess(result.message, "right", "top");
                            $("#commonlargeModal").modal('hide');
                        } else {
                            ams.common.ToastrError(result.message, "right", "top");
                        }
                    },
                })
                //Button Reverted From Loading
                revert();
            });
        }
    }
    this.Delete = function (id) {
        Swal.fire({
            title: 'Are you sure?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Yes',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    url: "/User/Delete/",
                    data: { id: id },
                    success: function (result) {
                        if (result.isSuccess) {
                            ams.user.TableId.ajax.reload(null, false); 
                            ams.common.ToastrSuccess(result.message, "right", "top");
                        } else {
                            ams.common.ToastrError(result.message, "right", "top");
                        }
                    },
                    error: function (xhr, status, error) {
                        ams.common.ToastrError("Something went wrong!", "right", "top");
                    }
                });
            }
        });
    }



}
    