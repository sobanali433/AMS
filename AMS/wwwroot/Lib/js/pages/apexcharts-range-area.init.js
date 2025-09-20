/*
Template Name: Velzon - Admin & Dashboard Template
Author: Themesbrand
Website: https://Themesbrand.com/
Contact: Themesbrand@gmail.com
File: range-area Chart init js
*/

// get colors array from the string
function getChartColorsArray(chartId) {
    if (document.getElementById(chartId) !== null) {
        var colors = document.getElementById(chartId).getAttribute("data-colors");
        colors = JSON.parse(colors);
        return colors.map(function (value) {
            var newValue = value.replace(" ", "");
            if (newValue.indexOf(",") === -1) {
                var color = getComputedStyle(document.documentElement).getPropertyValue(newValue);
                if (color) return color;
                else return newValue;;
            } else {
                var val = value.split(',');
                if (val.length == 2) {
                    var rgbaColor = getComputedStyle(document.documentElement).getPropertyValue(val[0]);
                    rgbaColor = "rgba(" + rgbaColor + "," + val[1] + ")";
                    return rgbaColor;
                } else {
                    return newValue;
                }
            }
        });
    }
}


// basic_range_area Chart
var rangeAreaBasicColors = getChartColorsArray("basic_range_area");
if (rangeAreaBasicColors) {
    var options = {
        series: [
            {
                name: 'New York Temperature',
                data: [
                    {
                        x: 'Jan',
                        y: [-2, 4]
                    },
                    {
                        x: 'Feb',
                        y: [-1, 6]
                    },
                    {
                        x: 'Mar',
                        y: [3, 10]
                    },
                    {
                        x: 'Apr',
                        y: [8, 16]
                    },
                    {
                        x: 'May',
                        y: [13, 22]
                    },
                    {
                        x: 'Jun',
                        y: [18, 26]
                    },
                    {
                        x: 'Jul',
                        y: [21, 29]
                    },
                    {
                        x: 'Aug',
                        y: [21, 28]
                    },
                    {
                        x: 'Sep',
                        y: [17, 24]
                    },
                    {
                        x: 'Oct',
                        y: [11, 18]
                    },
                    {
                        x: 'Nov',
                        y: [6, 12]
                    },
                    {
                        x: 'Dec',
                        y: [1, 7]
                    }
                ]
            }
        ],
        chart: {
            height: 350,
            type: 'rangeArea'
        },
        stroke: {
            curve: 'straight'
        },
        colors: rangeAreaBasicColors,
        title: {
            text: 'New York Temperature (all year round)'
        },
        markers: {
            hover: {
                sizeOffset: 5
            }
        },
        dataLabels: {
            enabled: false
        },
        yaxis: {
            labels: {
                formatter: (val) => {
                    return val + '°C'
                }
            }
        }
    };

    var chart = new ApexCharts(document.querySelector("#basic_range_area"), options);
    chart.render();
}


// combo_range_area Chart
var rangeAreaBasicColors = getChartColorsArray("otherplan_range_area");
if (rangeAreaBasicColors) {
    //$.ajax({
    //    type: "GET",
    //    url: UrlContent("Dashboard/GetBranchOtherPlanSummary?date=1 jan,2024-31 jan,2024"),
    //    success: function (data) {
    //        var options = {
    //            series: data.data,
    //            chart: {
    //                height: 350,
    //                type: 'line',
    //                stacked: false,
    //                animations: {
    //                    speed: 500
    //                }
    //            },
    //            colors: ['#7f4ca3', '#45a0e5', '#ff7c5e', '#4cb5ab', '#d94c6e', '#7dbb5e', '#f08a4b'],
    //            dataLabels: {
    //                enabled: false
    //            },
    //            stroke: {
    //                curve: 'straight',
    //            },
    //            legend: {
    //                show: true,
    //                customLegendItems: data.customLegendItems,
    //                inverseOrder: true
    //            },
    //            title: {
    //                text: 'All Other Plans Leads'
    //            },
    //            markers: {
    //                hover: {
    //                    sizeOffset: 5
    //                }
    //            }
    //        };

    //        var chart = new ApexCharts(document.querySelector("#otherplan_range_area"), options);
    //        chart.render();
    //    }
    //});
}
