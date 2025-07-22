# rawinput-cs
## C# wrapper for RawInput features from user32.dll
This was a very quick project and definitely has needed improvements, feel free to edit it however
### How to use
```
RawInput rawInput;

public Form1()
{
    InitializeComponent();
    rawInput = new RawInput();
    rawInput.OnKeyEvent += RawInput_OnKeyEvent;
    rawInput.OnMouseMove += RawInput_OnMouseMove;
    rawInput.OnMouseButton += RawInput_OnMouseButton;
}

protected override void OnHandleCreated(EventArgs e)
{
    base.OnHandleCreated(e);
    rawInput.Initialize(Handle);
}

protected override void WndProc(ref Message m)
{
    const int WM_INPUT = 0x00FF;

    if (m.Msg == WM_INPUT)
    {
        rawInput.ProcessInputMessage(m.LParam);
    }

    base.WndProc(ref m);
}

private void RawInput_OnKeyEvent(Keys key, bool isDown)
{
    label2.Text = $"Key {(isDown ? "down" : "up")}: {key}";
}

private void RawInput_OnMouseMove(int dx, int dy)
{
    label1.Text = $"Mouse moved: dx={dx}, dy={dy}";
}

private void RawInput_OnMouseButton(MouseButtons button, bool state)
{
    label1.Text = $"Mouse clicked: button={button}, state={state}";
}
```
