using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Goke.Core.Authentication
{
	public static class Certificate
	{
		public static X509Certificate2? LoadCert(string filename, string password, string thumbPrint/*, ILogger<Startup> logger*/)
		{
			X509Certificate2? cert;
			if (string.IsNullOrWhiteSpace(thumbPrint))
			{
				cert = LoadFileCert(filename, password);
			}
			else
			{
				cert = LoadStoreCert(thumbPrint);
			}

			return cert;
		}

		public static X509Certificate2? LoadStoreCert(string thumbPrint/*, ILogger<Startup> logger*/)
		{
			X509Certificate2? cert = null;
			using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				certStore.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection certCollection = certStore.Certificates.Find(
					X509FindType.FindByThumbprint,
					// Replace below with your cert's thumbprint
					thumbPrint,
					false);
				// Get the first cert with the thumbprint
				if (certCollection.Count > 0)
				{
					cert = certCollection[0];
					//logger.LogInformation($"Successfully loaded cert from registry: {cert.Thumbprint}");
				}
			}

			return cert;
		}

		public static X509Certificate2? LoadFileCert(string filename, string password)
		{
			X509Certificate2? cert=null;
			// Fallback to local file for development
			if (cert == null)
			{
				// const string Password = "jkb@M#16477";
				cert = new X509Certificate2(filename, password);
				//logger.LogInformation($"Falling back to cert from file. Successfully loaded: {cert.Thumbprint}");
				Console.WriteLine($"Falling back to cert from file. Successfully loaded: {cert.Thumbprint}");
			}

			return cert;
		}

		public static X509Certificate2 LoadByteCert(string filename, string password)
		{
			var bytes = File.ReadAllBytes(filename /*"~/arksolintapp.crt"*/);
			var cert = new X509Certificate2(bytes, password);
			return cert;
		}
	}
}
