// Call the dataTables jQuery plugin
$(document).ready(function () {
    var table = $('#dataTable').DataTable({
        /* Order severity at column 5 by ascending */
        "order": [5, 'asc']
    });


    /*Display error info to modal when on view click*/
    $('#dataTable tbody').on('click', '.view-error', function () {
        var data = table.row(this).data();

        /*Display error - currently workds for 12 headers (admin table)*/
        $("#modal-title").text(data[0] + " - " + data[5]);
        $("#modal-title").prepend("<i class='fa fa-eye'></i> ")

        $("#modal-message").text(data[8]);

        $("#modal-date").text(data[9]);
    });
});
