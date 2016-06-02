using System.Windows.Forms;

public delegate void MouseMovedEvent();

public class GlobalMouseHandler : IMessageFilter {
    private const int WM_MOUSEMOVE      = 0x0200;
    private const int WM_MOUSEWHEEL     = 0x020A;

    private const int WM_LBUTTONDOWN    = 0x0201;
    private const int WM_LBUTTONUP      = 0x0202;
    private const int WM_LBUTTONDBLCLK  = 0x0203;

    private const int WM_RBUTTONDOWN    = 0x0204;
    private const int WM_RBUTTONUP      = 0x0205;
    private const int WM_RBUTTONDBLCLK  = 0x0206;

    private const int WM_MBUTTONDOWN    = 0x0207;
    private const int WM_MBUTTONUP      = 0x0208;
    private const int WM_MBUTTONDBLCLK  = 0x0209;

    public event MouseMovedEvent MouseMoved;
    public event MouseMovedEvent LMBDoubleClick;

    #region IMessageFilter Members

    public bool PreFilterMessage(ref Message m) {
        if (m.Msg == WM_MOUSEMOVE) {
            if (MouseMoved != null) {
                MouseMoved();
            }
        } else if (m.Msg == WM_LBUTTONDBLCLK) {
            if (LMBDoubleClick != null) {
                LMBDoubleClick();
            }
        }
        // Always allow message to continue to the next filter control
        return false;
    }

    #endregion
}