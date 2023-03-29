function navigateToErrorTable(lob, integration) {
    var actionUrl = new URL('/ErrorTable/Index', window.location.origin);
    actionUrl.searchParams.append('lob', lob);
    actionUrl.searchParams.append('integration', integration);
    window.location.href = actionUrl.toString();
}