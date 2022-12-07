function SetModal() {

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                keyboard: true
                            },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });
    });
}

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#AddressTarget').load(result.url); // Carrega o resultado HTML para a div demarcada
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });

        SetModal();
        return false;
    });
}

function SearchZipCode() {
    $(document).ready(function () {

        function clear_form_zipCode() {
            // Limpa valores do formulário de cep.
            $("#Address_Street").val("");
            $("#Address_Neighborhood").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        //Quando o campo cep perde o foco.
        $("#Address_ZipCode").blur(function () {

            //Nova variável "cep" somente com dígitos.
            var zipCode = $(this).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (zipCode != "") {

                //Expressão regular para validar o CEP.
                var validatesZipCode = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validatesZipCode.test(zipCode)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("#Address_Street").val("...");
                    $("#Address_Neighborhood").val("...");
                    $("#Address_City").val("...");
                    $("#Address_State").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + zipCode + "/json/?callback=?",
                        function (dice) {

                            if (!("erro" in dice)) {
                                //Atualiza os campos com os valores da consulta.
                                $("#Address_Street").val(dice.logradouro);
                                $("#Address_Neighborhood").val(dice.bairro);
                                $("#Address_City").val(dice.localidade);
                                $("#Address_State").val(dice.uf);
                            } //end if.
                            else {
                                //CEP pesquisado não foi encontrado.
                                clear_form_zipCode();
                                alert("CEP não encontrado.");
                            }
                        });
                } //end if.
                else {
                    //cep é inválido.
                    clear_form_zipCode();
                    alert("Formato de CEP inválido.");
                }
            } //end if.
            else {
                //cep sem valor, limpa formulário.
                clear_form_zipCode();
            }
        });
    });
}

$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
});