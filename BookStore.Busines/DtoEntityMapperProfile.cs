﻿using AutoMapper;
using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Book;
using BookStore.Common.Dtos.Category;
using BookStore.Common.Dtos.Discount;
using BookStore.Common.Dtos.Language;
using BookStore.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Busines;

public class DtoEntityMapperProfile : Profile
{
    public DtoEntityMapperProfile()
    {
        CreateMap<AuthorCreate, Author>()
            .ForMember(dest => dest.Id,
            opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.Books, opt => opt.Ignore());
        CreateMap<AuthorUpdate, Author>();
        CreateMap<Author, AuthorGet>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Books, opt => opt.Ignore());
        CreateMap<Author, AuthorsList>();

        CreateMap<LanguageCreate, Language>()
            .ForMember(dest => dest.Id,
            opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.Books, opt => opt.Ignore());
        CreateMap<LanguageUpdate, Language>();
        CreateMap<Language, LanguageList>();
        CreateMap<Language, LanguageGet>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Books, opt => opt.Ignore());

        CreateMap<CategoryCreate, Category>()
            .ForMember(dest => dest.Id,
            opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.Books, opt => opt.Ignore());
        CreateMap<CategoryUpdate, Category>();
        CreateMap<Category, CategoriesList>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.SubCategories, opt => opt.Ignore());
        CreateMap<Category, CategoryListView>();
        CreateMap<Category, CategoryGet>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.Books, opt => opt.Ignore())
            .ForMember(dest => dest.SubCategories, opt => opt.Ignore());


        CreateMap<Book, BooksList>()
            .ForMember(dest => dest.Language,
            opt => opt.Ignore());

        CreateMap<BookCreate, Book>()
            .ForMember(dest => dest.Id,
            opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
            .ForMember(dest => dest.Categories, opt => opt.Ignore())
            .ForMember(dest => dest.Authors, opt => opt.Ignore())
            .ForMember(dest => dest.Language, opt => opt.Ignore())
            .ForMember(dest => dest.Orders, opt => opt.Ignore())
            .ForMember(dest => dest.Discount, opt => opt.Ignore());

        CreateMap<BookUpdate, Book>()
           .ForMember(dest => dest.Categories, opt => opt.Ignore())
           .ForMember(dest => dest.Authors, opt => opt.Ignore())
           .ForMember(dest => dest.Language, opt => opt.Ignore())
           .ForMember(dest => dest.Orders, opt => opt.Ignore())
           .ForMember(dest => dest.Discount, opt => opt.Ignore());

        CreateMap<Book, BookGet>()
           .ForMember(dest => dest.Categories, opt => opt.Ignore())
           .ForMember(dest => dest.Authors, opt => opt.Ignore())
           .ForMember(dest => dest.Language, opt => opt.Ignore())
           .ForMember(dest => dest.Discount, opt => opt.Ignore());


        CreateMap<DiscountCreate, Discount>()
            .ForMember(dest => dest.Book, opt => opt.Ignore());
        CreateMap<DiscountCreateByBook, Discount>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.Book, opt => opt.Ignore());
        CreateMap<DiscountUpdate, Discount>()
          .ForMember(dest => dest.Book, opt => opt.Ignore());
        CreateMap<Discount, DiscountsList>();
        CreateMap<Discount, DiscountGet>()
       .ForMember(dest => dest.Book, opt => opt.Ignore());
    }
}
