
var i = 0;
$('.dropdown').each(function () {
    i++;
    var newID = 'VarBtn' + i;
    $(this).attr('id', newID);
});

$(document).ready(() => {
    $('#btn').click(function () {
        var input = {};
        input.n = parseInt($('#quantity').val());
        input.prompt = $('#txt').val();
        input.size = $('#sel').find(":selected").val();
        $.ajax({
            url: '/Home/GenerateImage',
            method: 'post',
            contentType: 'application/json',
            data: JSON.stringify(input)

        }).done(function (data) {
            $.each(data.data, function () {

                var btnID = 'VarBtn_' + i;
                $('#display').append(
                    '<div class="col-md-3 p-10" style="padding-top:10px">' +
                    '<div class="dropdown">' +
                    '<button class="btnV" id="idOne">Dropdown</button>' +
                    '<div id="myDropdown" class="dropdown-content">' +
                    '<img hidden id="imgUrl" src="' + this.url + '">' +
                    '<br>Select size' +
                    '<select id="Vsel">' +
                    '<option selected>256x256</option>' +
                    '<option>512x512</option>' +
                    '<option>1024x1024</option>' +
                    '</select>' +
                    '<br>Enter quantity' +
                    '<input type="number" id="Vquantity" value="1" min="1" max="10" />' +
                    '<br>' +
                    '<button id="' + btnID + '" >Generate</button>' +
                    '</div>' +
                    '</div>' +
                    '<img class="p-10" src = "' + this.url + '">' +
                    '</div>');
            });
        });
    });

    /* Close the dropdown menu if the user clicks outside of it */
    window.onclick = function (event) {
        if (!event.target.matches('.dropdown-content')) {
            var dropdowns = document.getElementsByClassName("dropdown-content");
            var i;
            for (i = 0; i < dropdowns.length; i++) {
                var openDropdown = dropdowns[i];
                if (openDropdown.classList.contains('show')) {
                    openDropdown.classList.remove('show');
                }
            }
        }
    }
});

$(document.body).on('click', '[id^=VarBtn]', function () {

    var dropdown = $(this).closest('.dropdown-content');
    var input = {};

    input.size = dropdown.find('#Vsel').val();
    input.n = parseInt(dropdown.find('#Vquantity').val());
    input.image = dropdown.find('#imgUrl').attr("src");

    alert(input.image + "\n" + input.size + "\n" + input.n);

    //$.ajax({
    //    url: '/Home/Variation',
    //    method: 'post',
    //    contentType: "application/json",
    //    data: JSON.stringify(variationInput)

    //}).done(function (data) {

    //    var btnID = 'VarBtn_' + i;
    //    $.each(data.data, function () {
    //        $('#display').append(
    //            '<div class="col-md-3 p-10" style="padding-top:10px">' +
    //            '<div class="dropdown">' +
    //            '<button class="btnV" id="idOne">Dropdown</button>' +
    //            '<div id="myDropdown" class="dropdown-content">' +
    //            '<br>Select size' +
    //            '<select id="Vsel">' +
    //            '<option selected>256x256</option>' +
    //            '<option>512x512</option>' +
    //            '<option>1024x1024</option>' +
    //            '</select>' +
    //            '<br>Enter quantity' +
    //            '<input type="number" id="Vquantity" value="1" min="1" max="10" />' +
    //            '<br>' +
    //            '<button id="' + btnID +'">Generate</button>' +
    //            '</div>' +
    //            '</div>' +
    //            '<img class="p-10" id="GenImg" src = "' + this.url + '">' +
    //            '</div>');
    //    });
    //})
});