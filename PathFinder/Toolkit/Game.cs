using SharpDX.Direct3D;
using SharpDX.DXGI;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D3D11 = SharpDX.Direct3D11;

namespace PathFinder.Toolkit
{
    public class Game:IDisposable
    {
        private RenderForm renderForm;

        private const int Width = 1280;
        private const int Height = 720;

        private D3D11.Device d3dDevice;
        private D3D11.DeviceContext d3dDeviceContext;
        private SwapChain swapChain;
        private D3D11.RenderTargetView renderTargetView;

        ModeDescription backBufferDesc = new ModeDescription(Width, Height, new Rational(60, 1), Format.R8G8B8A8_UNorm);
        SwapChainDescription swapChainDesc;
       

        public Game()
        {
            renderForm = new RenderForm("TOTL");
            renderForm.ClientSize = new Size(Width, Height);
            renderForm.AllowUserResizing = false;

            swapChainDesc=new SwapChainDescription()
            {
                ModeDescription = backBufferDesc,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 1,
                OutputHandle = renderForm.Handle,
                IsWindowed = true
            };

            InitializeDeviceResources();
        }

        public void Run()
        {
            RenderLoop.Run(renderForm, RenderCallback);
        }

        private void RenderCallback()
        {
            Draw();
        }

        public void Dispose()
        {
           
            renderTargetView.Dispose();
            swapChain.Dispose();
            d3dDevice.Dispose();
            d3dDeviceContext.Dispose();
            renderForm.Dispose();
        }
        private void InitializeDeviceResources()
        {
            D3D11.Device.CreateWithSwapChain(DriverType.Hardware, D3D11.DeviceCreationFlags.None, swapChainDesc, out d3dDevice, out swapChain);
            d3dDeviceContext = d3dDevice.ImmediateContext;
            using (D3D11.Texture2D backBuffer = swapChain.GetBackBuffer<D3D11.Texture2D>(0))
            {
                renderTargetView = new D3D11.RenderTargetView(d3dDevice, backBuffer);
            }
        }

        private void Draw()
        {
            d3dDeviceContext.OutputMerger.SetRenderTargets(renderTargetView);
            d3dDeviceContext.ClearRenderTargetView(renderTargetView, new SharpDX.Color(32, 103, 178));
            swapChain.Present(1, PresentFlags.None);
        }
    }
}
