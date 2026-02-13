var ams = ams || {};

ams.product = new function () {
    this.Option = {
        Table: null,
        TableId: "",
        SearchId: "",
        RoleId: 0,
        AttendanceTable: null,
    }

    this.Init = function (options) {
        ams.product.Option = $.extend({}, ams.product.Option, options);
        ams.product.Option.Table = $("#productTableId").DataTable(
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
                    url: '/Product/GetList',
                    data: function (dtParms) {
                        dtParms.search.value = $("#txtSearch").val();

                        return dtParms;
                    },
                    dataSrc: function (json) {
                        return json.data; 
                    },
                    complete: function (response, result) {

                    }

                },
                "columns": [
                    {
                        data: null,
                        render: function (data, type, row) {
                            var renderResult = "", btnEdit = "";
                            //if (SNJAMS.User.Option.RoleId == SNJAMS.Common.Role.SuperAdmin || SNJAMS.User.Option.RoleId == SNSJAMS.Common.Role.HrManager || SNJAMS.User.Option.RoleId == SNJAMS.Common.Role.Finance || SNJAMS.User.Option.RoleId == SNJAMS.Common.Role.Recruiter) {
                            //renderResult += '<div class="form-check"><input type="checkbox" class="deleteAll mr-2 fs-0 form-check-input" value="' + data + '" onChange="SNJDC.User.OnSelectRecord()"/>';
                            renderResult += '<div class="form-check">';
                            renderResult += '&nbsp;<i class="fas fa-edit ml-2" style="cursor: pointer;" onclick="ams.product.Add(\'' + row.productId + '\',)"></i>';
                            renderResult += '&nbsp;<i class="fas fa-trash-alt ml-2" style="cursor: pointer;" onclick="ams.product.Delete(\'' + row.productId + '\',\'' + row.isActive + '\')"></i>';
                            //renderResult += '&nbsp;<a href="' + UrlContent("User/Detail/" + row.encryptUserMasterId) + '"><i class="fas fa-file ml-2" style="cursor: pointer;" ></i></a>';
                            renderResult += '</div>';

                            return renderResult;
                        }
                    },
                    { data: "productName", name: "ProductName" },
                    { data: "sku", name: "SKU" },
                    { data: "price", name: "Price" },
                    { data: "categoryName", name: "CategoryName" },
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

                ],
                order: [[0, "ASC"]],
            });
    }

    this.Add = function (id = '') {
        ams.common.HandleLoadingButton("#addNewproductBtnId", function (revert) {
            $.ajax({
                type: "GET",
                url: "/Product/_Details?id=" + id,
                success: function (data) {
                    $("#commonlargeModalContent").html(data);
                    ams.common.InitMask();
                    $.validator.unobtrusive.parse($("#AddProductform"));
                    $("#commonlargeModal").modal('show');
                    //HideLoader();
                    //Button Reverted From Loading
                    revert();
                }
            });
        });
    };

    this.Save = function () {
        if ($("#AddProductform").valid()) {
            //ShowLoader();
            var formdata = $("#AddProductform").serialize();
            ams.common.HandleLoadingButton("#saveproductButtonId", function (revert) {
                $.ajax({
                    type: "Post",
                    url: "/Product/Save/",
                    data: formdata,
                    success: function (result) {
                        //HideLoader();
                        if (result.isSuccess) {
                            ams.product.Option.Table.ajax.reload();
                            ams.common.ToastrSuccess(result.message, "right", "top");
                            $("#commonlargeModal").modal('hide');
                        } else {
                            ams.common.ToastrError(result.message, "right", "top");
                        }
                    },
                })
                revert();
            });
        }
    }

    this.Delete = function (id) {
        Swal.fire({
            title: 'Are you sure you want to deactivate this product?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#2ab57d',
            cancelButtonColor: '#fd625e',
            confirmButtonText: 'Yes',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                // Ajax call with correct URL
                ams.common.AjaxRequest('POST', '/Product/Delete', { id: id })
                    .then(result => {
                        if (result.isSuccess) {
                            const table = ams.product.Option.TableId;
                            const row = table.row('#row_' + id); // assuming each row has id="row_1" etc.
                            if (row.node()) {
                                const rowData = row.data();
                                rowData.isActive = !rowData.isActive; // toggle inactive
                                row.data(rowData).invalidate(); // update row
                            }

                            ams.common.ToastrSuccess(result.message, "right", "top");
                        } else {
                            ams.common.ToastrError(result.message, "right", "top");
                        }
                    })
                    .catch(err => {
                        console.error(err);
                        ams.common.ToastrError("Something went wrong!", "right", "top");
                    });
            }
        });
    }

}