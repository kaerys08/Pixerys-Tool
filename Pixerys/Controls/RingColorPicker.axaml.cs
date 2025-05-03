using Avalonia.Controls.Metadata;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Media;
using System;

namespace Pixerys.Controls;

[TemplatePart("PART_Selector", typeof(Ellipse))]
public class RingColorPicker : TemplatedControl
{
    Ellipse? selector;

    private double centerX;
    private double centerY;

    private bool IsToggled = false;

    public static readonly DirectProperty<RingColorPicker, bool> ShowProperty =
        AvaloniaProperty.RegisterDirect<RingColorPicker, bool>(
            nameof(Show),
            o => o.Show,
            (o, v) => o.Show = v,
            defaultBindingMode: BindingMode.TwoWay);

    private bool _show;

    public bool Show
    {
        get { return _show; }
        set { SetAndRaise(ShowProperty, ref _show, value); }
    }

    public static readonly DirectProperty<RingColorPicker, double> PosXProperty =
        AvaloniaProperty.RegisterDirect<RingColorPicker, double>(
            nameof(PosX),
            o => o.PosX,
            (o, v) => o.PosX = v,
            defaultBindingMode: BindingMode.TwoWay);

    private double _posX;

    public double PosX
    {
        get { return _posX; }
        set { SetAndRaise(PosXProperty, ref _posX, value); }
    }

    public static readonly DirectProperty<RingColorPicker, double> PosYProperty =
        AvaloniaProperty.RegisterDirect<RingColorPicker, double>(
            nameof(PosY),
            o => o.PosY,
            (o, v) => o.PosY = v,
            defaultBindingMode: BindingMode.TwoWay);

    private double _posY;

    public double PosY
    {
        get { return _posY; }
        set { SetAndRaise(PosYProperty, ref _posY, value); }
    }

    public static readonly DirectProperty<RingColorPicker, double> SelectorAngleProperty =
        AvaloniaProperty.RegisterDirect<RingColorPicker, double>(
            nameof(SelectorAngle),
            o => o.SelectorAngle,
            (o, v) => o.SelectorAngle = v,
            defaultBindingMode: BindingMode.TwoWay);

    private double _selectorAngle;

    public double SelectorAngle
    {
        get { return _selectorAngle; }
        set { SetAndRaise(SelectorAngleProperty, ref _selectorAngle, value); }
    }

    public static readonly DirectProperty<RingColorPicker, double> SnappedAngleProperty =
        AvaloniaProperty.RegisterDirect<RingColorPicker, double>(
            nameof(SnappedAngle),
            o => o.SnappedAngle,
            (o, v) => o.SnappedAngle = v,
            defaultBindingMode: BindingMode.TwoWay);

    private double _snappedAngle;

    public double SnappedAngle
    {
        get { return _selectorAngle; }
        set { SetAndRaise(SnappedAngleProperty, ref _snappedAngle, value); }
    }

    public static readonly DirectProperty<RingColorPicker, Color> GetColorProperty =
        AvaloniaProperty.RegisterDirect<RingColorPicker, Color>(
            nameof(GetColor),
            o => o.GetColor,
            (o, v) => o.GetColor = v,
            defaultBindingMode: BindingMode.TwoWay);

    private Color _getColor;

    public Color GetColor
    {
        get { return _getColor; }
        set { SetAndRaise(GetColorProperty, ref _getColor, value); }
    }

    public static readonly DirectProperty<RingColorPicker, byte> RComponentProperty =
        AvaloniaProperty.RegisterDirect<RingColorPicker, byte>(
            nameof(RComponent),
            o => o.RComponent,
            defaultBindingMode: BindingMode.OneWayToSource);

    private byte _rComponent = 0;

    public byte RComponent
    {
        get { return _rComponent; }
        private set { SetAndRaise(RComponentProperty, ref _rComponent, value); }
    }

    public static readonly DirectProperty<RingColorPicker, byte> GComponentProperty =
        AvaloniaProperty.RegisterDirect<RingColorPicker, byte>(
            nameof(GComponent),
            o => o.GComponent,
            defaultBindingMode: BindingMode.OneWayToSource);

    private byte _gComponent = 0;

    public byte GComponent
    {
        get { return _gComponent; }
        private set { SetAndRaise(GComponentProperty, ref _gComponent, value); }
    }

    public static readonly DirectProperty<RingColorPicker, byte> BComponentProperty =
        AvaloniaProperty.RegisterDirect<RingColorPicker, byte>(
            nameof(BComponent),
            o => o.BComponent,
            defaultBindingMode: BindingMode.OneWayToSource);

    private byte _bComponent = 0;

    public byte BComponent
    {
        get { return _bComponent; }
        private set { SetAndRaise(BComponentProperty, ref _bComponent, value); }
    }

    private readonly ConicGradientBrush RingRGBConicGradientBrush = new()
    {
        GradientStops =
        [
            new GradientStop(new Color(255,255,255,0), 0.0),
            new GradientStop(new Color(255,0,255,0), 0.165),
            new GradientStop(new Color(255,0,255,255), 0.33),
            new GradientStop(new Color(255,0,0,255), 0.495),
            new GradientStop(new Color(255,255,0,255), 0.66),
            new GradientStop(new Color(255,255,0,0), 0.825),
            new GradientStop(new Color(255,255,255,0), 1.0),
        ],
    };

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        selector = e.NameScope.Find("PART_Selector") as Ellipse;

        if (selector != null)
        {
            selector.Stroke = RingRGBConicGradientBrush;
            selector.IsVisible = Show;
            selector.StrokeThickness = selector.Width / 10.5;
            selector.PointerPressed += Selector_PointerPressed;
            selector.PointerMoved += Selector_PointerMoved;
            selector.PointerReleased += Selector_PointerReleased;
            centerX = (selector.Width / 2);
            centerY = (selector.Height / 2);
            SelectorAngle = 89;
            RComponent = (byte)(GetColor.R >> 3);
            GComponent = (byte)(GetColor.G >> 3);
            BComponent = (byte)(GetColor.B >> 3);
        }
    }

    private void Selector_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        IsToggled = true;
    }

    private void Selector_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        Point pos = e.GetPosition(this);
        double angle = Math.Atan2(pos.Y - centerY, pos.X - centerY) * (180 / Math.PI);

        if (IsToggled == true)
        {
            PosX = pos.X - centerX;
            PosY = pos.Y - centerY;
            SelectorAngle = 360 - (180 - angle);
            RComponent = (byte)(GetColor.R >> 3);
            GComponent = (byte)(GetColor.G >> 3);
            BComponent = (byte)(GetColor.B >> 3);
        }
    }

    private void Selector_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        IsToggled = false;
    }
}