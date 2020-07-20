$(document).ready(function () {

    function limpa_formulário_cep() {

        $("#Logradouro").val("");
        $("#Complemento").val("");
        $("#Bairro").val("");
        $("#Localidade").val("");
        $("#Uf").val("");
        $("#Unidade").val("");
        $("#Ibge").val("");
        $("#Gia").val("");


    }

    $("#Cep").blur(function () {
        var cep = $(this).val().replace(/\D/g, '');
        if (cep != "") {

            var validacep = /^[0-9]{8}$/;
            if (validacep.test(cep)) {

                $("#Logradouro").val("...");
                $("#Complemento").val("...");
                $("#Bairro").val("...");
                $("#Localidade").val("...");
                $("#Uf").val("...");
                $("#Unidade").val("...");
                $("#Ibge").val("...");
                $("#Gia").val("...");

                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {

                        $("#Logradouro").val(dados.logradouro);
                        $("#Bairro").val(dados.bairro);
                        $("#Localidade").val(dados.localidade);
                        $("#Uf").val(dados.uf);
                        $("#Unidade").val(dados.unidade);
                        $("#Ibge").val(dados.ibge);
                        $("#Gia").val(dados.gia);


                    }
                    else {

                        limpa_formulário_cep();
                        alert("CEP não encontrado.");
                    }
                });
            }
            else {

                limpa_formulário_cep();
                alert("Formato de CEP inválido.");
            }
        }
        else {

            limpa_formulário_cep();
        }
    });
});
