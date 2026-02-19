var ams = ams || {};

ams.order = new function () {
    this.Option = {
        Table: null,
        TableId: "",
        SearchId: "",
        RoleId: 0,
        AttendanceTable: null,
    }

    this.Init = function (options) {
        ams.order.Option = $.extend({}, ams.order.Option, options);
        ams.order.Option.Table = $("#orderTableId").DataTable(
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
                    url: '/Order/GetList',
                    data: function (dtParms) {
                        dtParms.search.value = $("#txtSearch").val();
                        return dtParms;
                    },
                    complete: function (response, result) { }
                },
                "columns": [
                    {
                        data: "OrderId", name: "OrderId", orderable: false, render: function (data, type, row) {

                            var renderResult = "", btnEdit = "";
                            //if (ams.order.Option.RoleName == "SuperAdmin") {
                            renderResult += '<div class="form-check">';
                            renderResult += '<div class="form-check"><input type="checkbox" class="deleteAll mr-2 fs-0 form-check-input" value="' + data + '" onChange="ams.order.OnSelectRecord()"/>';
                            //renderResult += '<div class="form-check">';
                            renderResult += '&nbsp;<i class="fas fa-eye ml-2"  style="cursor: pointer;" onclick="ams.order.Add(\'' + row.user + '\',)"></i>';
                            //renderResult += '&nbsp;<i class="fas fa-trash-alt ml-2" style="cursor: pointer;" onclick="ams.user.Delete(\'' + row.userMasterId + '\',\'' + row.isActive + '\')"></i>';
                            //renderResult += '&nbsp;<a href="' + UrlContent("User/Detail/" + row.encryptUserMasterId) + '"><i class="fas fa-file ml-2" style="cursor: pointer;" ></i></a>';
                            //renderResult += '</div>';

                            return renderResult;
                        }
                    },
                    { data: "productName", name: "ProductName" },

                    {
                        data: "branchName",
                        name: "branchName",
                        className: "text-center col-2",
                        render: function (data, type, row) {
                            return '<span style="Color:#102557; padding:4px 8px; border-radius:4px; display:inline-block;">'
                                + data +
                                '</span>';
                        }
                    },


                    //{ data: "branchName", name: "BranchName" },
                    { data: "quantity", name: "Quantity" },
                    //{ data: "orderType", name: "OrderType" },
                    {
                        data: "orderType", name: "orderType", className: "text-center col-1",
                        render: function (data, type, row) {
                            var badge = ''
                            if (row.orderType == "IN")
                                badge += '<span class="badge bg-success-subtle text-success">Stock In</span>'
                            else
                                badge += '<span class="badge bg-danger-subtle text-danger">Stock OUT</span>'
                            return badge;
                        }
                    },
                    { data: "createdAt", name: "CreatedAt" },
                ],
                order: [[0, "ASC"]],
            });
    }

    this.OnSelectRecord = function () {
        //checked unchecked header checkbox
        var totalRowLength = $(".deleteAll").length;
        var totalSelectedRowLength = $(".deleteAll:checked").length;

        if (parseInt(totalRowLength) == parseInt(totalSelectedRowLength)) {
            $(".selectAll").prop("checked", true);
        }
        else {
            $(".selectAll").prop("checked", false);
        }
        if (totalSelectedRowLength > 0) {
            //$("#btnAssignToAgent").removeClass("hide")
        }
        else {
            //$("#btnAssignToAgent").addClass("hide")
        }
    }

    $('.selectAll').change(function () {
        var ischecked = $('.selectAll').is(':checked');
        if (ischecked) {
            $('.deleteAll').prop('checked', true);
        }
        if (!ischecked) {
            $('.deleteAll').prop('checked', false);
        }
        var totalSelectedRowLength = $(".deleteAll:checked").length;
        if (totalSelectedRowLength > 0) {
            //$("#btnAssignToAgent").removeClass("hide")
        }
        else {
            //$("#btnAssignToAgent").addClass("hide")
        }
    });
}