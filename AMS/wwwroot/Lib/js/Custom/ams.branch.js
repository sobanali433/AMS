var ams = ams || {};

ams.branch = new function () {
    this.Option = {
        Table: null,
        TableId: "",
        SearchId: "",
        RoleId: 0,
        AttendanceTable: null,
    }

    this.Init = function (options) {
        ams.branch.Option = $.extend({}, ams.branch.Option, options);
        ams.branch.Option.Table = $("#branchTableId").DataTable(
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
                    url: '/BranchMaster/GetList',
                    data: function (dtParms) {
                        dtParms.search.value = $("#txtSearch").val();
                        return dtParms;
                    },
                    complete: function (response, result) { }
                },
                "columns": [
                    {
                        data: "branchId", name: "branchId", orderable: false, render: function (data, type, row) {

                            var renderResult = "", btnEdit = "";
                            //if (SNJAMS.branch.Option.RoleId == SNJAMS.Common.Role.SuperAdmin || SNJAMS.branch.Option.RoleId == SNSJAMS.Common.Role.HrManager || SNJAMS.branch.Option.RoleId == SNJAMS.Common.Role.Finance || SNJAMS.branch.Option.RoleId == SNJAMS.Common.Role.Recruiter) {
                            //renderResult += '<div class="form-check"><input type="checkbox" class="deleteAll mr-2 fs-0 form-check-input" value="' + data + '" onChange="SNJDC.branch.OnSelectRecord()"/>';
                            renderResult += '<div class="form-check">';
                            renderResult += '&nbsp;<i class="fas fa-edit ml-2" style="cursor: pointer;" onclick="ams.branch.Add(\'' + row.branchId + '\',)"></i>';
                            renderResult += '&nbsp;<i class="fas fa-trash-alt ml-2" style="cursor: pointer;" onclick="ams.branch.Delete(\'' + row.branchId + '\',\'' + row.isActive + '\')"></i>';
                            //renderResult += '&nbsp;<a href="' + UrlContent("branch/Detail/" + row.encryptbranchMasterId) + '"><i class="fas fa-file ml-2" style="cursor: pointer;" ></i></a>';
                            renderResult += '</div>';

                            return renderResult;
                        }
                    },
                    { data: "branchName", name: "branchName" },
                    { data: "location", name: "Location" },
                    { data: "createdAt", name: "createdAt" },
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


                    //{ data: "branchname", name: "branchname", },



                    //{ data: "firstName", name: "FirstName", },
                    //{ data: "lastName", name: "LastName" },

                    //{ data: "contactNumber", name: "ContactNumber" },

                ],
                order: [[0, "ASC"]],
            });
    }
    this.Add = function (id = '') {
        ams.common.HandleLoadingButton("#addNewBranchBtnId", function (revert) {
            $.ajax({
                type: "GET",
                url: "/BranchMaster/_Details?id=" + id,
                success: function (data) {
                    $("#common-md-DialogContent").html(data);
                    ams.common.InitMask();
                    $.validator.unobtrusive.parse($("#Addbranchform"));
                    $("#common-md-dialog").modal('show');
                    //HideLoader();
                    //Button Reverted From Loading
                    revert();
                }
            });
        });
    };
    this.Save = function () {
        if ($("#Addbranchform").valid()) {
            //ShowLoader();
            var formdata = $("#Addbranchform").serialize();
            ams.common.HandleLoadingButton("#saveBranchButtonId", function (revert) {
                $.ajax({
                    type: "Post",
                    url: "/BranchMaster/Save/",
                    data: formdata,
                    success: function (result) {
                        //HideLoader();
                        if (result.isSuccess) {
                            ams.branch.Option.Table.ajax.reload();
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
                    url: "/BranchMaster/Delete/",
                    data: { id: id },
                    success: function (result) {
                        if (result.isSuccess) {
                            ams.branch.TableId.ajax.reload(null, false);
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
