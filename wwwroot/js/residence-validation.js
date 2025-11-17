// Custom client-side validation for Residence
$(document).ready(function () {
    // Custom validation for built year
    $.validator.addMethod("validdate", function (value, element) {
        if (!value) return true; // Let required validation handle empty
        
        var currentYear = new Date().getFullYear();
        var minYear = currentYear - 150;
        var year = parseInt(value);
        
        return !isNaN(year) && year >= minYear && year <= currentYear;
    }, function (params, element) {
        var currentYear = new Date().getFullYear();
        var minYear = currentYear - 150;
        return `Built year must be between ${minYear} and ${currentYear}`;
    });

    // Custom validation for bathrooms
    $.validator.addMethod("validbathrooms", function (value, element) {
        if (!value) return true; // Let required validation handle empty
        
        var num = parseFloat(value);
        if (isNaN(num)) return false;
        
        // Check if it's integer or ends with .5
        return num % 1 === 0 || num % 1 === 0.5;
    }, "Bathrooms must be a whole number or end with .5 (e.g., 1, 1.5, 2, 2.5)");

    // Apply custom validation
    $("form").validate({
        rules: {
            BuiltYear: {
                validdate: true
            },
            BathroomNumber: {
                validbathrooms: true
            }
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") == "BuiltYear" || element.attr("name") == "BathroomNumber") {
                error.insertAfter(element.next(".form-text"));
            } else {
                error.insertAfter(element);
            }
        }
    });

    // Real-time validation feedback
    $('#builtYear').on('blur', function () {
        var currentYear = new Date().getFullYear();
        var minYear = currentYear - 150;
        var year = parseInt($(this).val());
        
        if ($(this).val() && (!isNaN(year) && (year < minYear || year > currentYear))) {
            $(this).addClass('is-invalid');
        } else {
            $(this).removeClass('is-invalid').addClass('is-valid');
        }
    });

    $('#BathroomNumber').on('blur', function () {
        var num = parseFloat($(this).val());
        if ($(this).val() && (!isNaN(num) && (num % 1 !== 0 && num % 1 !== 0.5))) {
            $(this).addClass('is-invalid');
        } else if ($(this).val()) {
            $(this).removeClass('is-invalid').addClass('is-valid');
        }
    });

    // Real-time validation for name (alphanumeric only)
    $('#Name').on('blur', function () {
        var name = $(this).val();
        var alphanumericRegex = /^[a-zA-Z0-9\s]+$/;
        
        if (name && !alphanumericRegex.test(name)) {
            $(this).addClass('is-invalid');
        } else if (name) {
            $(this).removeClass('is-invalid').addClass('is-valid');
        }
    });
});