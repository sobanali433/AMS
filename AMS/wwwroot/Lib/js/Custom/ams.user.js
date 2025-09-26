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
        //this.Option = options;

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
                        data: "username", name: "userName"
                    },
                    { data: "lastName", name: "LastName" },
                    { data: "firstName", name: "FirstName" },
                    //{ data: "contactnumber", name: "ContactNumber" },
                    {
                        data: "roleName", name: "roleName", render: function (data, type, row) {
                            if (row.roleId == 1) {
                                return '<span class="badge bg-success">' + data + '</span>';
                            } else {
                                return '<span class="badge bg-primary">' + data + '</span>';
                            }
                        }
                    },                   
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
                    {
                        data: null,
                        render: function (data, type, row) {
                            return `<button class="btn btn-sm btn-primary">Edit</button>`;
                        }
                    }
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


}
    