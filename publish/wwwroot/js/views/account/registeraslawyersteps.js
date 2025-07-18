var currentTab = 0; // Current tab is set to be the first tab (0)
showTab(currentTab); // Display the current tab

function showTab(n) {
    // This function will display the specified tab of the form...
    var x = document.getElementsByClassName("tab");
    x[n].style.display = "block";
    //... and fix the Previous/Next buttons:
    if (n == 0) {
        document.getElementById("prevBtn").style.display = "none";
    } else {
        document.getElementById("prevBtn").style.display = "inline";
    }
    if (n == (x.length - 1)) {
        document.getElementById("save").style.display = "inline";
        document.getElementById("nextBtn").style.display = "none";

    } else {
        document.getElementById("save").style.display = "none";
        document.getElementById("nextBtn").style.display = "inline";
    }
    //... and run a function that will display the correct step indicator:
    fixStepIndicator(n)
}

function nextPrev(n) {
    // This function will figure out which tab to display
    var x = document.getElementsByClassName("tab");
    // Exit the function if any field in the current tab is invalid:
    if ((currentTab + n) == 1 && !validateForm()) return false;
    if ((currentTab + n) == 2 && !validateFormChecks()) return false;
    if ((currentTab + n) == 3 && !validateFormChecks()) return false;
    // Hide the current tab:
    x[currentTab].style.display = "none";
    // Increase or decrease the current tab by 1:
    currentTab = currentTab + n;
    // if you have reached the end of the form...
    if (currentTab >= x.length) {
        // ... the form gets submitted:
        document.getElementById("register_lawyer").submit();
        return false;
    }
    // Otherwise, display the correct tab:
    showTab(currentTab);
}
function validateFormChecks() {
    // This function deals with validation of the form fields
    var x, y, z, i, valid = true;
    x = document.getElementsByClassName("tab");
    y = x[currentTab].getElementsByClassName("valiate-input");
    z = x[currentTab].getElementsByClassName("valiate-label");
    // A loop that checks every input field in the current tab:
    var validCount = 0;
    for (i = 0; i < y.length; i++) {
        // If a field is empty...
        if (y[i].checked == false) {
            // add an "invalid" class to the field:
            y[i].className += " invalid";
            z[i].className += " invalid";
            // and set the current valid status to false
            valid = false;
        } else {
            validCount++;
        }
    }
    // If the valid status is true, mark the step as finished and valid:
    if (validCount > 0) {
        var valuesToPost = [];
        $.each($('input[name="Specialities[]"]:checked'), function (index, value) {
            valuesToPost.push(value.value);
        });
        var formData = new FormData();
        formData.append("values", valuesToPost);
        $.ajax({
            url: `/especialidades-temas/get/v3`,
            type: "POST",
            async: false,
            processData: false,  // tell jQuery not to process the data
            contentType: false,   // tell jQuery not to set contentType
            data: formData,
        }).done(function (result) {
            $("#themes-container").html(result);
            valid = true;
            document.getElementsByClassName("step")[currentTab].className += " finish";
        }).fail(function (e) {
            valid = false;
        });
    }
    return valid; // return the valid status
}
function validateForm() {
    // This function deals with validation of the form fields
    var x, y, z, i, valid = true;
    x = document.getElementsByClassName("tab");
    y = x[currentTab].getElementsByClassName("valiate-input");
    z = x[currentTab].getElementsByClassName("valiate-label");
    // A loop that checks every input field in the current tab:
    for (i = 0; i < y.length; i++) {
        // If a field is empty...
        if (y[i].value == "" || y[i].value == null) {
            // add an "invalid" class to the field:
            y[i].className += " invalid";
            z[i].className += " invalid";
            // and set the current valid status to false
            valid = false;
        }
        if (document.getElementById('Password').value !=
            document.getElementById('ConfirmPassword').value) {
            y[i].className += " invalid";
            z[i].className += " invalid";
            valid = false;
        }
        if (document.getElementById('TermsAndConditions').checked == false) {
            y[i].className += " invalid";
            z[i].className += " invalid";
            valid = false;
        }
    }
    // If the valid status is true, mark the step as finished and valid:
    if (valid) {
        document.getElementsByClassName("step")[currentTab].className += " finish";
    }
    return valid; // return the valid status
}

function fixStepIndicator(n) {
    // This function removes the "active" class of all steps...
    var i, x = document.getElementsByClassName("step");
    for (i = 0; i < x.length; i++) {
        x[i].className = x[i].className.replace(" active", "");
    }
    //... and adds the "active" class on the current step:
    x[n].className += " active";
}