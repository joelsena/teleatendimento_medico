// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function reloadPage() {
  window.location.reload();
}

function handleOnSubmit(form) {
  const data = new FormData(form);

  const body = {
    nome: data.get("pessoa.nome"),
    cpf: data.get("pessoa.cpf"),
    endereco: {
      logradouro: data.get("pessoa.endereco.logradouro"),
      numero: data.get("pessoa.endereco.numero"),
      cep: data.get("pessoa.endereco.cep"),
      bairro: data.get("pessoa.endereco.bairro"),
      cidade: data.get("pessoa.endereco.cidade"),
      estado: data.get("pessoa.endereco.estado"),
    },
  };

  $.ajax({
    url: "Home/Insert",
    type: "POST",
    data: body,
    success: reloadPage,
  });
}

function deletePessoa(id) {
  const res = confirm("Deseja realmente excluir essa pessoa?");
  if (res) {
    $.ajax({
      url: "Home/Delete",
      type: "POST",
      data: { id },
      success: reloadPage,
    });
  }
}

function handleUpdatePessoa(form, pessoaId, enderecoId) {
  const data = new FormData(form);

  const body = {
    id: pessoaId,
    nome: data.get("pessoa.nome"),
    cpf: data.get("pessoa.cpf"),
    endereco: {
      id: enderecoId,
      logradouro: data.get("pessoa.endereco.logradouro"),
      numero: data.get("pessoa.endereco.numero"),
      cep: data.get("pessoa.endereco.cep"),
      bairro: data.get("pessoa.endereco.bairro"),
      cidade: data.get("pessoa.endereco.cidade"),
      estado: data.get("pessoa.endereco.estado"),
    },
  };

  $.ajax({
    url: "Home/Update",
    type: "POST",
    data: body,
    success: reloadPage,
  });
}

function updatePessoa(id) {
  $.ajax({
    url: `Home/Get/${id}`,
    type: "GET",
    success: (response) => {
      $("#pessoa_nome").val(response.nome);
      $("#pessoa_cpf").val(response.cpf);
      $("#pessoa_endereco_logradouro").val(response.endereco.logradouro);
      $("#pessoa_endereco_numero").val(response.endereco.numero);
      $("#pessoa_endereco_cep").val(response.endereco.cep);
      $("#pessoa_endereco_bairro").val(response.endereco.bairro);
      $("#pessoa_endereco_cidade").val(response.endereco.cidade);
      $("#pessoa_endereco_estado").val(response.endereco.estado);

      document.getElementById("form-button").innerHTML = "Atualizar";

      // $("#form-button").attr("innerText", "Atualizar");
      $("#form-action").attr(
        "onsubmit",
        `event.preventDefault(); handleUpdatePessoa(this, pessoaId=${response.id},enderecoId=${response.endereco.id})`
      );
    },
  });
}
