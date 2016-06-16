using System.Windows.Forms;

public delegate void MouseMovedEvent();

public class GlobalMouseHandler : IMessageFilter {
    private const int WmMousemove      = 0x0200;
    private const int WmMousewheel     = 0x020A;

    private const int WmLbuttondown    = 0x0201;
    private const int WmLbuttonup      = 0x0202;
    private const int WmLbuttondblclk  = 0x0203;

    private const int WmRbuttondown    = 0x0204;
    private const int WmRbuttonup      = 0x0205;
    private const int WmRbuttondblclk  = 0x0206;

    private const int WmMbuttondown    = 0x0207;
    private const int WmMbuttonup      = 0x0208;
    private const int WmMbuttondblclk  = 0x0209;

    public event MouseMovedEvent MouseMoved;
    public event MouseMovedEvent LmbDoubleClick;

    #region IMessageFilter Members

    public bool PreFilterMessage(ref Message m) {
        if (m.Msg == WmMousemove) {
            if (MouseMoved != null) {
                MouseMoved();
            }
        } else if (m.Msg == WmLbuttondblclk) {
            if (LmbDoubleClick != null) {
                LmbDoubleClick();
            }
        }
        // Always allow message to continue to the next filter control
        return false;
    }

    #endregion
}