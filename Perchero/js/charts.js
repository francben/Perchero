
$(document).ready(function () {
	'use strict';


    /***** BAR CHART *****/

    //var bardata = [['Jan', 10], ['Feb', 23], ['Mar', 18], ['Apr', 13], ['May', 17], ['Jun', 30],
	//								 ['Jul', 26], ['Aug', 16], ['Sep', 17], ['Oct', 5], ['Nov', 8], ['Dec', 15]];


    $.plot('#barchart',  datos , {
		  series: {
            lines: {
              lineWidth: 1
            },
				bars: {
					show: true,
					barWidth: 0.5,
					align: 'center',
               lineWidth: 0,
               fillColor: '#428BCA'
				}
		  },
        grid: {
            borderColor: '#ddd',
            borderWidth: 1,
            labelMargin: 10
		  },
		  xaxis: {
				mode: 'categories',
				tickLength: 0
		  }
	 });


    /***** PIE CHART *****/

    var piedata = [
        { label: 'Series 1', data: [[1,10]], color: '#D9534F'},
        { label: 'Series 2', data: [[1,30]], color: '#1CAF9A'},
        { label: 'Series 3', data: [[1,90]], color: '#F0AD4E'},
        { label: 'Series 4', data: [[1,70]], color: '#428BCA'},
        { label: 'Series 5', data: [[1,80]], color: '#5BC0DE'}
	 ];

    $.plot('#piechart', piedata, {
        series: {
            pie: {
                show: true,
                radius: 1,
                label: {
                    show: true,
                    radius: 2/3,
                    formatter: labelFormatter,
                    threshold: 0.1
                }
            }
        },
        grid: {
            hoverable: true,
            clickable: true
        }
    });

    function labelFormatter(label, series) {
		return '<div style="font-size:8pt; text-align:center; padding:2px; color:white;">' + label + '<br>' + Math.round(series.percent) + '%</div>';
	}


});
