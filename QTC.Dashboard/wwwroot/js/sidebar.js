// Save the selected organization in the local storage
function saveSelectedOrg(organizationLob) {
    localStorage.setItem("selectedOrganization", organizationLob);
}

// Save the selected integration in the local storage
function saveSelectedIntegration(integration) {
    localStorage.setItem("selectedIntegration", integration);
}

// Apply the active state on the sidebar items based on the local storage
function applyActiveState() {
    const selectedOrganization = localStorage.getItem("selectedOrganization");
    const selectedIntegration = localStorage.getItem("selectedIntegration");

    if (selectedOrganization) {
        const organizationSelector = `a[href="/ErrorTable/Index?lob=${selectedOrganization}"]`;
        const organizationLink = document.querySelector(organizationSelector);

        if (organizationLink) {
            organizationLink.classList.add("active");
            const collapseDiv = organizationLink.parentElement.querySelector('.collapse');
            collapseDiv.classList.add("show");

            if (selectedIntegration) {
                const integrationSelector = `a[href="/ErrorTable/Index?lob=${selectedOrganization}&integration=${selectedIntegration}"]`;
                const integrationLink = document.querySelector(integrationSelector);

                if (integrationLink) {
                    integrationLink.classList.add("active");
                }
            }
        }
    }
}

// Initialize the active state when the page loads
document.addEventListener("DOMContentLoaded", applyActiveState);
