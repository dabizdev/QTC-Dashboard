// Call the dataTables jQuery plugin
$(document).ready(function () {
    var table = $('#dataTable').DataTable({
        /* Order severity at column 5 by ascending */
        "order": [5, 'asc']
    });
});
