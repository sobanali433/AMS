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
                    complete: function (response, result) { }

                },
                "columns": [
                    {
                        data: null,
                        render: function (data, type, row) {

                            return `<button class="btn btn-sm btn">  <i class="ri-delete-bin-fill align-bottom text-muted"></i></button>`;
                        }
                    },
                    { data: "productName", name: "ProductName" },
                    //{ data: "productType", name: "ProductType" },
                    { data: "sku", name: "SKU" },
                    { data: "price", name: "Price" },
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
}