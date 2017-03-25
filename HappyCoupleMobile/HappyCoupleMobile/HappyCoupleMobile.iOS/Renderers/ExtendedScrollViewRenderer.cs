using System;
using System.ComponentModel;
using HappyCoupleMobile.iOS.Custom;
using HappyCoupleMobile.iOS.Renderers;
using HappyCoupleMobile.Mvvm.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using PointF = CoreGraphics.CGPoint;
using RectangleF = CoreGraphics.CGRect;

[assembly: ExportRenderer(typeof(ExtendedScrollView), typeof(ExtendedScrollViewRenderer))]
namespace HappyCoupleMobile.iOS.Renderers
{
    public class ExtendedScrollViewRenderer : UIScrollView, IVisualElementRenderer, IEffectControlProvider
    {
        private EventTracker _events;
        private VisualElementPackager _packager;
        private RectangleF _previousFrame;
        private ScrollToRequestedEventArgs _requestedScroll;
        private VisualElementTracker _tracker;

        public IScrollViewController Controller => (IScrollViewController)Element;
        public ScrollView ScrollView => Element as ScrollView;
        public VisualElement Element { get; private set; }
        public UIViewController ViewController => null;
        public UIView NativeView => this;

        public event EventHandler<VisualElementChangedEventArgs> ElementChanged;

        public ExtendedScrollViewRenderer() : base(RectangleF.Empty)
        {
            ScrollAnimationEnded += HandleScrollAnimationEnded;
            Scrolled += HandleScrolled;
        }

        public SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            return NativeView.GetSizeRequest(widthConstraint, heightConstraint, 44, 44);
        }

        public void SetElement(VisualElement element)
        {
            _requestedScroll = null;
            var oldElement = Element;
            Element = element;

            if (oldElement != null)
            {
                oldElement.PropertyChanged -= HandlePropertyChanged;
                ((IScrollViewController)oldElement).ScrollToRequested -= OnScrollToRequested;
            }

            if (element != null)
            {
                element.PropertyChanged += HandlePropertyChanged;
                ((IScrollViewController)element).ScrollToRequested += OnScrollToRequested;
                if (_packager == null)
                {
                    DelaysContentTouches = true;

                    _packager = new VisualElementPackager(this);
                    _packager.Load();

                    _tracker = new VisualElementTracker(this);
                    _events = new EventTracker(this);
                    _events.LoadEvents(this);
                }

                UpdateContentSize();
                UpdateBackgroundColor();

                OnElementChanged(new VisualElementChangedEventArgs(oldElement, element));

                Custom.Helpers.RegisterEffectControlProvider(this, oldElement, element);

                if (!string.IsNullOrEmpty(element.AutomationId))
                {
                    AccessibilityIdentifier = element.AutomationId;
                }
            }
        }

        public void SetElementSize(Size size)
        {
            Layout.LayoutChildIntoBoundingRegion(Element, new Rectangle(Element.X, Element.Y, size.Width, size.Height));
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            if (_requestedScroll != null && Superview != null)
            {
                var request = _requestedScroll;
                _requestedScroll = null;
                OnScrollToRequested(this, request);
            }

            if (_previousFrame != Frame)
            {
                _previousFrame = Frame;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_packager == null)
                {
                    return;
                }

                SetElement(null);

                _packager.Dispose();
                _packager = null;

                _tracker.Dispose();
                _tracker = null;

                _events.Dispose();
                _events = null;

                ScrollAnimationEnded -= HandleScrollAnimationEnded;
                Scrolled -= HandleScrolled;
            }

            base.Dispose(disposing);
        }

        protected virtual void OnElementChanged(VisualElementChangedEventArgs e)
        {
            var changed = ElementChanged;
            changed?.Invoke(this, e);
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ScrollView.ContentSizeProperty.PropertyName)
            {
                UpdateContentSize();
            }
            else if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
            {
                UpdateBackgroundColor();
            }
        }

        private void HandleScrollAnimationEnded(object sender, EventArgs e)
        {
            Controller.SendScrollFinished();
        }

        private void HandleScrolled(object sender, EventArgs e)
        {
            UpdateScrollPosition();
        }

        private void OnScrollToRequested(object sender, ScrollToRequestedEventArgs e)
        {
            if (Superview == null)
            {
                _requestedScroll = e;
                return;
            }
            if (e.Mode == ScrollToMode.Position)
            {
                SetContentOffset(new PointF((nfloat)e.ScrollX, (nfloat)e.ScrollY), e.ShouldAnimate);
            }
            else
            {
                var positionOnScroll = Controller.GetScrollPositionForElement(e.Element as VisualElement, e.Position);

                positionOnScroll.X = positionOnScroll.X.Clamp(0, ContentSize.Width - Bounds.Size.Width);
                positionOnScroll.Y = positionOnScroll.Y.Clamp(0, ContentSize.Height - Bounds.Size.Height);

                switch (ScrollView.Orientation)
                {
                    case ScrollOrientation.Horizontal:
                        SetContentOffset(new PointF((nfloat)positionOnScroll.X, ContentOffset.Y), e.ShouldAnimate);
                        break;
                    case ScrollOrientation.Vertical:
                        SetContentOffset(new PointF(ContentOffset.X, (nfloat)positionOnScroll.Y), e.ShouldAnimate);
                        break;
                    case ScrollOrientation.Both:
                        SetContentOffset(new PointF((nfloat)positionOnScroll.X, (nfloat)positionOnScroll.Y), e.ShouldAnimate);
                        break;
                }
            }
            if (!e.ShouldAnimate)
            {
                Controller.SendScrollFinished();
            }
        }

        private void UpdateBackgroundColor()
        {
            BackgroundColor = Element.BackgroundColor.ToUIColor(Color.Transparent);
        }

        private void UpdateContentSize()
        {
            var contentSize = ((ScrollView)Element).ContentSize.ToSizeF();
            if (!contentSize.IsEmpty)
            {
                ContentSize = contentSize;
            }
        }

        private void UpdateScrollPosition()
        {
            if (ContentOffset.Y == 0)
            {
                return;
            }

            if (ScrollView != null)
            {
                Controller.SetScrolledPosition(ContentOffset.X, ContentOffset.Y);
            }
        }

        void IEffectControlProvider.RegisterEffect(Effect effect)
        {
        }
    }
}