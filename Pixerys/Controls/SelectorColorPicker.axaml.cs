using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Metadata;

namespace Pixerys.Controls;

[TemplatePart("PART_SelectorPresenter", typeof(Ellipse))]
public class SelectorColorPicker : TemplatedControl
{
    Ellipse? selectorPresenter;

    public static readonly DirectProperty<SelectorColorPicker, double> CenterXProperty =
        AvaloniaProperty.RegisterDirect<SelectorColorPicker, double>(
            nameof(CenterX),
            o => o.CenterX,
            (o, v) => o.CenterX = v,
            defaultBindingMode: BindingMode.TwoWay);

    private double _centerX;

    public double CenterX
    {
        get { return _centerX; }
        set { SetAndRaise(CenterXProperty, ref _centerX, value); }
    }

    public static readonly DirectProperty<SelectorColorPicker, double> CenterYProperty =
        AvaloniaProperty.RegisterDirect<SelectorColorPicker, double>(
            nameof(CenterY),
            o => o.CenterY,
            (o, v) => o.CenterY = v,
            defaultBindingMode: BindingMode.TwoWay);

    private double _centerY;

    public double CenterY
    {
        get { return _centerY; }
        set { SetAndRaise(CenterYProperty, ref _centerY, value); }
    }
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        selectorPresenter = e.NameScope.Find("PART_SelectorPresenter") as Ellipse;

        if (selectorPresenter != null)
        {
            CenterX = selectorPresenter.Width / 2;
            CenterY = selectorPresenter.Height / 2;
            selectorPresenter.StrokeThickness = selectorPresenter.Width * 0.2;
        }
    }
}