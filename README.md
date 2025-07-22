# rawinput-cs
## C# wrapper for RawInput features from `user32.dll`
This is a very minimal wrapper and definitely needs improvements, edit it however you want. Note that this only covers a small amount of events (mouse move and click, keyboard press) and others like mouse wheel are not included and you must implement them yourself if you want to use them. 

### What is Raw Input?
Raw Input is a Windows API that lets applications receive unprocessed and low level input data directly from keyboards, mice, and other HIDs.
*for example: if you turn it on in a game, Windows' default mouse acceleration will be ineffective as it now receives the data from `RawInput`*

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
