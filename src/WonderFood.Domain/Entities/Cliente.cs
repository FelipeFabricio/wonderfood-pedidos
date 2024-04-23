﻿using System.Text.RegularExpressions;

namespace WonderFood.Domain.Entities;

public class Cliente
{
    public Guid Id { get; init; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cpf { get; private set; }

    public Cliente(string nome, string email, string cpf)
    {
        Id = Guid.NewGuid();
        ValidarNome(nome);
        ValidarEmail(email);
        ValidarCpf(cpf);
    }

    private void ValidarCpf(string cpf)
    {
        cpf = RetornarCpfSemFormatacao(cpf);

        // Verificar se o CPF possui 11 dígitos
        if (cpf.Length != 11)
            throw new ArgumentException("CPF deve conter 11 dígitos");

        // Verificar se todos os dígitos são iguais
        if (cpf.Distinct().Count() == 1)
            throw new ArgumentException("CPF inválido");

        // Calcula o primeiro dígito verificador
        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += int.Parse(cpf[i].ToString()) * (10 - i);
        int remainder = sum % 11;
        int digit1 = remainder < 2 ? 0 : 11 - remainder;

        // Verifica o primeiro dígito verificador
        if (int.Parse(cpf[9].ToString()) != digit1)
            throw new ArgumentException("CPF inválido");

        // Calcula o segundo dígito verificador
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += int.Parse(cpf[i].ToString()) * (11 - i);
        remainder = sum % 11;
        int digit2 = remainder < 2 ? 0 : 11 - remainder;

        // Verifica o segundo dígito verificador
        if (int.Parse(cpf[10].ToString()) != digit2)
            throw new ArgumentException("CPF inválido");

        Cpf = cpf;
    }

    private static string RetornarCpfSemFormatacao(string cpf)
    {
        cpf = new string(cpf.Where(char.IsDigit).ToArray());
        return cpf;
    }

    private void ValidarEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        if(Regex.IsMatch(email, pattern))
            Email = email;
        else
            throw new ArgumentException("O E-mail informado é inválido.");
    }

    private void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome) || nome.Length < 3)
            throw new ArgumentException("O nome do Cliente não pode possuir menos de 3 caracteres.");

        if (Regex.IsMatch(nome, @"\d"))
            throw new ArgumentException("O nome do Cliente não pode conter números.");

        if (Regex.IsMatch(nome, @"[^a-zA-Z\s]"))
            throw new ArgumentException("O nome do Cliente não pode conter caracteres especiais.");

        Nome = nome;
    }

    public override string ToString() => $"{Cpf.Substring(0, 3)}.{Cpf.Substring(3, 3)}.{Cpf.Substring(6, 3)}-{Cpf.Substring(9, 2)}";
}

    