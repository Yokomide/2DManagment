using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Text;
using UnityEngine.EventSystems;

public class ProgramVisualEditor : MonoBehaviour
{
    [SerializeField] private GameObject _lineReference;
    [Range(5, 15)] public int LinesAmount = 5;

    [SerializeField] private GameObject _numpadPanel;

    private List<CustomInputField> _lines = new List<CustomInputField>();
    private int _currentLineIndex;


    private void Start()
    {
        for (int i = 0; i < LinesAmount; i++)
        {
            GameObject line = Instantiate(_lineReference, Vector2.zero, Quaternion.identity, transform);
            CustomInputField fieldComponent = line.GetComponent<CustomInputField>();
            fieldComponent.Handler = this;
            fieldComponent.LineNumber.text = i.ToString();
            _lines.Add(fieldComponent);
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
        _lines[0].FocusIt();
        ProgramEditor.Instance.commands = new Command[LinesAmount];
    }


    public void OpenNumpad()
    {
        _numpadPanel.SetActive(true);
    }

    public void CloseNumpad()
    {
        _numpadPanel.SetActive(false);
    }


    public void WriteMoveForward()
    {
        WriteCommand(new MoveForvardCommand());
    }

    public void WriteTurnLeft()
    {
        WriteCommand(new TurnLeftCommand()); 
    }

    public void WriteTurnRight()
    {
        WriteCommand(new TurnRightCommand());
    }

    public void WritePickUp()
    {
        WriteCommand(new PickUpCommand());
    }

    public void WritePutDown()
    {
        WriteCommand(new PutDownCommand());
    }

    public void WriteCycle(int iterations)
    {
        WriteCommand(new CycleCommand(iterations));
        WriteCommand(new EndCommand());
    }


    private string GetTab(string command)
    {
        int tab = 0;
        for (int i = 0; i <= _currentLineIndex; i++)
        {
            foreach (var ch in _lines[i].Text.text)
            {
                if (ch == '{')
                {
                    tab += 1;
                }
                else if (ch == '}')
                {
                    tab -= 1;
                }
            }
        }

        if (command == "}")
        {
            tab -= 1;
        }

        tab = Mathf.Clamp(tab, 0, 100);
        string strTab = "";
        for (int i = 0; i < tab; i++)
        {
            strTab += " ";
        }
        return strTab;
    }

    private void WriteCommand(Command command)
    {
        CustomInputField line = _lines[_currentLineIndex];
        string str = GetTab(command.ForWrite) + command.ForWrite;

        line.RemoveCaret();
        if (string.IsNullOrEmpty(line.Text.text))
        {
            ProgramEditor.Instance.commands[_currentLineIndex] = command;
            line.Text.text = str;
        }
        else if (string.IsNullOrEmpty(_lines[_lines.Count - 1].Text.text))
        {
            for (int i = _lines.Count - 1; i > _currentLineIndex; i--)
            {
                _lines[i].Text.text = _lines[i - 1].Text.text;
                ProgramEditor.Instance.commands[i] = ProgramEditor.Instance.commands[i - 1];
            }
            ProgramEditor.Instance.commands[_currentLineIndex + 1] = command;
            _lines[_currentLineIndex + 1].Text.text = str;
            _lines[_currentLineIndex == _lines.Count - 1 ? _currentLineIndex : _currentLineIndex + 1].FocusIt();
        }
    }

    public void EraseLine()
    {
        for (int i = _currentLineIndex; i < _lines.Count - 1; i++)
        {
            _lines[i].Text.text = _lines[i + 1].Text.text;
            ProgramEditor.Instance.commands[i] = ProgramEditor.Instance.commands[i + 1];
        }
        _lines[_lines.Count - 1].Text.text = string.Empty;
        ProgramEditor.Instance.commands[_lines.Count - 1] = null;
        _lines[_currentLineIndex == 0 ? _currentLineIndex : _currentLineIndex - 1].FocusIt();
    }

    private void UpdateIndex(CustomInputField field)
    {
        for (int i = 0; i < _lines.Count; i++)
        {
            CustomInputField line = _lines[i];
            if (line == field)
            {
                _currentLineIndex = i;
                break;
            }
        }
    }

    public void OnFocused(CustomInputField field)
    {
        foreach (var line in _lines)
        {
            line.StopAnimateCaret();
        }
        field.AnimateCaret();
        UpdateIndex(field);
    }
}
