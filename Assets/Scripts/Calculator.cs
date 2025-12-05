using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{
    public TMP_Text displayText;

    private string input = "";
    private int firstNumber = 0;
    private string currentOperator = "";
    private bool isNewInput = true;

    // Number buttons 0-9
    public void PressNumber(string number)
    {
        if (isNewInput)
        {
            input = "";
            isNewInput = false;
        }

        input += number;
        UpdateDisplay();
    }

    // Delete / Backspace
    public void PressDelete()
    {
        if (input.Length > 0)
        {
            input = input.Substring(0, input.Length - 1);
            UpdateDisplay();
        }
    }

    // Operator buttons: +, -, *, /
    public void PressOperator(string op)
    {
        if (!int.TryParse(input, out int num)) return;

        if (currentOperator != "")
        {
            Calculate();
        }
        else
        {
            firstNumber = num;
        }

        currentOperator = op;
        isNewInput = true;
    }

    // Equals button
    public void PressEquals()
    {
        if (!int.TryParse(input, out int secondNumber)) return;

        switch (currentOperator)
        {
            case "+": firstNumber += secondNumber; break;
            case "-": firstNumber -= secondNumber; break;
            case "*": firstNumber *= secondNumber; break;
            case "/": firstNumber /= secondNumber; break;
        }

        input = firstNumber.ToString();
        currentOperator = "";
        isNewInput = true;
        UpdateDisplay();
    }

    // Clear button
    public void PressClear()
    {
        input = "";
        firstNumber = 0;
        currentOperator = "";
        isNewInput = true;
        UpdateDisplay();
    }

    void Calculate()
    {
        if (!int.TryParse(input, out int secondNumber)) return;

        switch (currentOperator)
        {
            case "+": firstNumber += secondNumber; break;
            case "-": firstNumber -= secondNumber; break;
            case "*": firstNumber *= secondNumber; break;
            case "/": firstNumber /= secondNumber; break;
        }

        input = firstNumber.ToString();
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (displayText != null)
            displayText.text = input == "" ? "0" : input;
    }
}