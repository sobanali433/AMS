var ams = ams || {};

ams.stock = new function () {
    this.Option = {
        Table: null,
        TableId: "",
        SearchId: "",
        RoleId: 0,
        AttendanceTable: null,
    }

    this.Init = function (options) {
        ams.stock.Option = $.extend({}, ams.stock.Option, options);
        ams.stock.Option.Table = $("#stockTableId").DataTable(
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
                    url: '/Stock/GetList',
                    data: function (d) {
                        d.branchId = $('#branchSelect').val();
                    },
                    
                
                    complete: function (response, result) { }
                },
                "columns": [
                    {
                        data: "stockId", name: "stockId", orderable: false, render: function (data, type, row) {

                            var renderResult = "", btnEdit = "";
                            //if (SNJAMS.User.Option.RoleId == SNJAMS.Common.Role.SuperAdmin || SNJAMS.User.Option.RoleId == SNSJAMS.Common.Role.HrManager || SNJAMS.User.Option.RoleId == SNJAMS.Common.Role.Finance || SNJAMS.User.Option.RoleId == SNJAMS.Common.Role.Recruiter) {
                            //renderResult += '<div class="form-check"><input type="checkbox" class="deleteAll mr-2 fs-0 form-check-input" value="' + data + '" onChange="SNJDC.User.OnSelectRecord()"/>';
                            renderResult += '<div class="form-check">';
                            renderResult += '&nbsp;<i class="fas fa-edit ml-2" style="cursor: pointer;" onclick="ams.stock.Add(\'' + row.stockId + '\',)"></i>';
                            renderResult += '&nbsp;<i class="fas fa-trash-alt ml-2" style="cursor: pointer;" onclick="ams.stock.Delete(\'' + row.stockId + '\',\'' + row.isActive + '\')"></i>';
                            renderResult += '</div>';

                            return renderResult;
                        }
                    },
                    { data: "branchName", name: "branchName" },
                    { data: "productName", name: "ProductName" },
                    { data: "quantity", name: "Quantity" },
                    { data: "lastUpdated", name: "LastUpdated" },
                ],
                order: [[0, "ASC"]],
            });
        $('#branchSelect').on('change', function () {
            ams.stock.Option.Table.ajax.reload();
        });
    }


    this.Add = function (id = '') {
        ams.common.HandleLoadingButton("#addNewStockBtnId", function (revert) {
            $.ajax({
                type: "GET",
                url: "/Stock/ManageStock?id=" + id,
                success: function (data) {
                    $("#commonlargeModalContent").html(data);
                    ams.common.InitMask();
                    $.validator.unobtrusive.parse($("#AddStockform"));
                    $("#commonlargeModal").modal('show');
                    //HideLoader();
                    //Button Reverted From Loading
                    revert();
                }
            });
        });
    };

    this.Save = function () {
        if ($("#AddStockform").valid()) {
            //ShowLoader();
            var formdata = $("#AddStockform").serialize();
            ams.common.HandleLoadingButton("#saveStockformButtonId", function (revert) {
                $.ajax({
                    type: "Post",
                    url: "/Stock/ManageStock/",
                    data: formdata,
                    success: function (result) {
                        //HideLoader();
                        if (result.isSuccess) {
                            ams.stock.Option.Table.ajax.reload();
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