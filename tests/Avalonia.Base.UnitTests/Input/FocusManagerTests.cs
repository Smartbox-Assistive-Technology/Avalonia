using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using Avalonia.UnitTests;
using Xunit;

namespace Avalonia.Base.UnitTests.Input
{
    public class FocusManagerTests
    {
        [Fact]
        public void Next_Continue_Returns_Next_Control_In_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button1" },
                            (current = new Button { Name = "Button2" }),
                            (next = new Button { Name = "Button3" }),
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };
            var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            //var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Continue_Returns_First_Control_In_Next_Sibling_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button1" },
                            new Button { Name = "Button2" },
                            (current = new Button { Name = "Button3" }),
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            (next = new Button { Name = "Button4" }),
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Skips_Unfocusable_Siblings()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button1" },
                            new Button { Name = "Button2" },
                            new StackPanel
                            {
                                Children =
                                {
                                    (current = new Button { Name = "Button3" }),
                                }
                            },
                            new TextBlock { Name = "TextBlock" },
                            (next = new Button { Name = "Button4" }),
                        }
                    },
                    new Button { Name = "Button5" },
                    new Button { Name = "Button6" },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Continue_Doesnt_Enter_Panel_With_TabNavigation_None()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            (next = new Button { Name = "Button1" }),
                            new Button { Name = "Button2" },
                            (current = new Button { Name = "Button3" }),
                        }
                    },
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.None,
                        Children =
                        {
                            new StackPanel
                            {
                                Children =
                                {
                                    new Button { Name = "Button4" },
                                    new Button { Name = "Button5" },
                                    new Button { Name = "Button6" },
                                }
                            },
                        }
                    }
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Continue_Returns_Next_Sibling()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button1" },
                            new Button { Name = "Button2" },
                            (current = new Button { Name = "Button3" }),
                        }
                    },
                    (next = new Button { Name = "Button4" }),
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Skips_Non_TabStop_Siblings()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button1" },
                            new Button { Name = "Button2" },
                            (current = new Button { Name = "Button3" }),
                            new Button { Name="Button4", [KeyboardNavigation.IsTabStopProperty] = false }
                        }
                    },
                    (next = new Button { Name = "Button5" }),
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Continue_Returns_First_Control_In_Next_Uncle_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new StackPanel
                            {
                                Children =
                                {
                                    new Button { Name = "Button1" },
                                    new Button { Name = "Button2" },
                                    (current = new Button { Name = "Button3" }),
                                }
                            },
                        },
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            (next = new Button { Name = "Button4" }),
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Continue_Returns_Child_Of_Top_Level()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    (next = new Button { Name = "Button1" }),
                }
            };

            var result = KeyboardNavigationHandler.GetNext(top, NavigationDirection.Next);

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Continue_Wraps()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new StackPanel
                            {
                                Children =
                                {
                                    (next = new Button { Name = "Button1" }),
                                    new Button { Name = "Button2" },
                                    new Button { Name = "Button3" },
                                }
                            },
                        },
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            (current = new Button { Name = "Button6" }),
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Cycle_Returns_Next_Control_In_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                        Children =
                        {
                            new Button { Name = "Button1" },
                            (current = new Button { Name = "Button2" }),
                            (next = new Button { Name = "Button3" }),
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Cycle_Wraps_To_First()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                        Children =
                        {
                            (next = new Button { Name = "Button1" }),
                            new Button { Name = "Button2" },
                            (current = new Button { Name = "Button3" }),
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Contained_Returns_Next_Control_In_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Contained,
                        Children =
                        {
                            new Button { Name = "Button1" },
                            (current = new Button { Name = "Button2" }),
                            (next = new Button { Name = "Button3" }),
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Contained_Stops_At_End()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Contained,
                        Children =
                        {
                            new Button { Name = "Button1" },
                            new Button { Name = "Button2" },
                            (current = new Button { Name = "Button3" }),
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Null(result);
        }

        [Fact]
        public void Next_Once_Moves_To_Next_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Once,
                        Children =
                        {
                            new Button { Name = "Button1" },
                            (current = new Button { Name = "Button2" }),
                            new Button { Name = "Button3" },
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            (next = new Button { Name = "Button4" }),
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_Once_Moves_To_Active_Element()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            StackPanel container;
            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    (container = new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Once,
                        Children =
                        {
                            new Button { Name = "Button1" },
                            (next = new Button { Name = "Button2" }),
                            new Button { Name = "Button3" },
                        }
                    }),
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            (current = new Button { Name = "Button6" }),
                        }
                    },
                }
            };

            KeyboardNavigation.SetTabOnceActiveElement(container, next);

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_None_Moves_To_Next_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.None,
                        Children =
                        {
                            new Button { Name = "Button1" },
                            (current = new Button { Name = "Button2" }),
                            new Button { Name = "Button3" },
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            (next = new Button { Name = "Button4" }),
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Next_None_Skips_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            StackPanel container;
            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    (container = new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.None,
                        Children =
                        {
                            new Button { Name = "Button1" },
                            new Button { Name = "Button2" },
                            new Button { Name = "Button3" },
                        }
                    }),
                    new StackPanel
                    {
                        Children =
                        {
                            (next = new Button { Name = "Button4" }),
                            new Button { Name = "Button5" },
                            (current = new Button { Name = "Button6" }),
                        }
                    },
                }
            };

            KeyboardNavigation.SetTabOnceActiveElement(container, next);

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Continue_Returns_Previous_Control_In_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button1" },
                            (next = new Button { Name = "Button2" }),
                            (current = new Button { Name = "Button3" }),
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Continue_Returns_Last_Control_In_Previous_Sibling_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button1" },
                            new Button { Name = "Button2" },
                            (next = new Button { Name = "Button3" }),
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            (current = new Button { Name = "Button4" }),
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Continue_Returns_Last_Child_Of_Sibling()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button1" },
                            new Button { Name = "Button2" },
                            (next = new Button { Name = "Button3" }),
                        }
                    },
                    (current = new Button { Name = "Button4" }),
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Continue_Returns_Last_Control_In_Previous_Nephew_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new StackPanel
                            {
                                Children =
                                {
                                    new Button { Name = "Button1" },
                                    new Button { Name = "Button2" },
                                    (next = new Button { Name = "Button3" }),
                                }
                            },
                        },
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            (current = new Button { Name = "Button4" }),
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Continue_Wraps()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new StackPanel
                            {
                                Children =
                                {
                                    (current = new Button { Name = "Button1" }),
                                    new Button { Name = "Button2" },
                                    new Button { Name = "Button3" },
                                }
                            },
                        },
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            (next = new Button { Name = "Button6" }),
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Continue_Returns_Parent()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;

            var top = new Decorator
            {
                Focusable = true,
                Child = current = new Button
                {
                    Name = "Button",
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(top, result);
        }

        [Fact]
        public void Previous_Cycle_Returns_Previous_Control_In_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                        Children =
                        {
                            (next = new Button { Name = "Button1" }),
                            (current = new Button { Name = "Button2" }),
                            new Button { Name = "Button3" },
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Cycle_Wraps_To_Last()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                        Children =
                        {
                            (current = new Button { Name = "Button1" }),
                            new Button { Name = "Button2" },
                            (next = new Button { Name = "Button3" }),
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Contained_Returns_Previous_Control_In_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Contained,
                        Children =
                        {
                            (next = new Button { Name = "Button1" }),
                            (current = new Button { Name = "Button2" }),
                            new Button { Name = "Button3" },
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Contained_Stops_At_Beginning()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Contained,
                        Children =
                        {
                            (current = new Button { Name = "Button1" }),
                            new Button { Name = "Button2" },
                            new Button { Name = "Button3" },
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4" },
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Null(result);
        }

        [Fact]
        public void Previous_Once_Moves_To_Previous_Container()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button1" },
                            new Button { Name = "Button2" },
                            (next = new Button { Name = "Button3" }),
                        }
                    },
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Once,
                        Children =
                        {
                            new Button { Name = "Button4" },
                            (current = new Button { Name = "Button5" }),
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Once_Moves_To_Active_Element()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            StackPanel container;
            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    (container = new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Once,
                        Children =
                        {
                            new Button { Name = "Button1" },
                            (next = new Button { Name = "Button2" }),
                            new Button { Name = "Button3" },
                        }
                    }),
                    new StackPanel
                    {
                        Children =
                        {
                            (current = new Button { Name = "Button4" }),
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            KeyboardNavigation.SetTabOnceActiveElement(container, next);

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Once_Moves_To_First_Element()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button current;
            Button next;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Once,
                        Children =
                        {
                            (next = new Button { Name = "Button1" }),
                            new Button { Name = "Button2" },
                            new Button { Name = "Button3" },
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            (current = new Button { Name = "Button4" }),
                            new Button { Name = "Button5" },
                            new Button { Name = "Button6" },
                        }
                    },
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Equal(next, result);
        }

        [Fact]
        public void Previous_Contained_Doesnt_Select_Child_Control()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Decorator current;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Contained,
                Children =
                {
                    (current = new Decorator
                    {
                        Focusable = true,
                        Child = new Button(),
                    })
                }
            };

            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Previous, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Null(result);
        }

        [Fact]
        public void Respects_TabIndex_Moving_Forwards()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button start;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button1", TabIndex = 5 },
                            (start = new Button { Name = "Button2", TabIndex = 2 }),
                            new Button { Name = "Button3", TabIndex = 1 },
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4", TabIndex = 3  },
                            new Button { Name = "Button5", TabIndex = 6  },
                            new Button { Name = "Button6", TabIndex = 4  },
                        }
                    },
                }
            };

            var result = new List<string?>();
            var current = (IInputElement)start;

            do
            {
                result.Add(((Control)current).Name);
                current = KeyboardNavigationHandler.GetNext(current, NavigationDirection.Next);
            } while (current is object && current != start);

            Assert.Equal(new[]
            {
                "Button2", "Button4", "Button6", "Button1", "Button5", "Button3"
            }, result);
        }

        [Fact]
        public void Respects_TabIndex_Moving_Backwards()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button start;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button1", TabIndex = 5 },
                            (start = new Button { Name = "Button2", TabIndex = 2 }),
                            new Button { Name = "Button3", TabIndex = 1 },
                        }
                    },
                    new StackPanel
                    {
                        Children =
                        {
                            new Button { Name = "Button4", TabIndex = 3  },
                            new Button { Name = "Button5", TabIndex = 6  },
                            new Button { Name = "Button6", TabIndex = 4  },
                        }
                    },
                }
            };

            var result = new List<string?>();
            var current = (IInputElement)start;

            do
            {
                result.Add(((Control)current).Name);
                current = KeyboardNavigationHandler.GetNext(current, NavigationDirection.Previous);
            } while (current is not null && current != start);

            Assert.Equal(new[]
            {
                "Button2", "Button3", "Button5", "Button1", "Button6", "Button4"
            }, result);
        }

        [Fact]
        public void Cannot_Focus_Child_Of_Disabled_Control()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            Button start;
            Button expected;

            var top = new StackPanel
            {
                [KeyboardNavigation.TabNavigationProperty] = KeyboardNavigationMode.Cycle,
                Children =
                {
                    (start = new Button { Name = "Button1" }),
                    new Border
                    {
                        IsEnabled = false,
                        Child = new Button { Name = "Button2" },
                    },
                    (expected = new Button { Name = "Button3" }),
                }
            };

            var current = (IInputElement)start;
            var topLevel = new TestRoot() { Child = top };var result = topLevel.FocusManager.FindNextElement(NavigationDirection.Next, new FindNextElementOptions() {  FocusedElement = current });

            Assert.Same(expected, result);
        }

        [Fact]
        public void Focuses_First_Child_From_No_Focus()
        {
            using var testApp = UnitTestApplication.Start(TestServices.RealFocus);

            using var app = UnitTestApplication.Start(TestServices.RealFocus);
            var button = new Button();
            var root = new TestRoot(button);
            var target = new KeyboardNavigationHandler();

            target.SetOwner(root);

            root.RaiseEvent(new KeyEventArgs
            {
                RoutedEvent = InputElement.KeyDownEvent,
                Key = Key.Tab,
            });

            Assert.True(button.IsFocused);
        }
    }
}
