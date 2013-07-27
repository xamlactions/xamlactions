using NUnit.Framework;
using XamlActions.Http;

namespace XamlActions.Tests {
    public class UriHelperTests {
        private UriHelper _builder;

        [Test]
        public void Should_create_uri_from_url() {
            _builder = new UriHelper("http://localhost/");
            Assert.AreEqual("http://localhost/", _builder.Build().ToString());
        }

        [Test]
        public void Should_create_uri_from_url_and_add_path() {
            _builder = new UriHelper("http://localhost/");
            _builder.AddPath("api/timeline");
            Assert.AreEqual("http://localhost/api/timeline", _builder.Build().ToString());
        }

        [Test]
        public void Should_create_uri_with_path_from_url_and_add_more_path() {
            _builder = new UriHelper("http://localhost/api");
            _builder.AddPath("timeline");
            Assert.AreEqual("http://localhost/api/timeline", _builder.Build().ToString());
        }

        [Test]
        public void Should_create_uri_with_path_from_url_and_add_params() {
            _builder = new UriHelper("http://localhost/api");
            _builder.AddPath("timeline");
            _builder.AddParam("par1", "1");
            _builder.AddParam("par2", "2");
            Assert.AreEqual("http://localhost/api/timeline?par1=1&par2=2", _builder.Build().ToString());
        }
    }
}