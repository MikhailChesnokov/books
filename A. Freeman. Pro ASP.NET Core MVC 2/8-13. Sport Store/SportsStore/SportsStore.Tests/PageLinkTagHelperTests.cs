namespace SportsStore.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Models.ViewModels;
    using Moq;
    using Xunit;



    public class PageLinkTagHelperTests
    {
        [Fact]
        public void CanGeneratePageLinks()
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                     .Returns("Test/Page1")
                     .Returns("Test/Page2")
                     .Returns("Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(x => x.GetUrlHelper(It.IsAny<ActionContext>())).Returns(urlHelper.Object);

            var tagHelper = new PageLinkTagHelper(urlHelperFactory.Object)
            {
                PageModel = new PagingInfo
                {
                    CurrentPage = 2,
                    TotalItems = 28,
                    ItemsPerPage = 10
                },
                PageAction = "Test"
            };

            var context = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), string.Empty);
            var content = new Mock<TagHelperContent>();
            var output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult(content.Object));


            tagHelper.Process(context, output);


            Assert.Equal(
                @"<a href=""Test/Page1"">1</a>" +
                @"<a href=""Test/Page2"">2</a>" +
                @"<a href=""Test/Page3"">3</a>",
                output.Content.GetContent());

        }
    }
}