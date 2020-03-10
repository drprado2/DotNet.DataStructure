using System;
using System.Collections.Generic;
using System.Linq;
using DotNet.DataStructure.Linear.Arrays;
using DotNet.DataStructure.Linear.Tests.Fakes;
using DotNet.DataStructure.Linear.Tests.Fakes.FakeModels;
using DotNet.DataStructure.Shared;
using FluentAssertions;
using Xunit;

namespace DotNet.DataStructure.Linear.Tests.Arrays
{
    public class ArrayListTest
    {
        private readonly KeyGenerator _keyGenerator;

        public ArrayListTest()
        {
            _keyGenerator = new KeyGenerator();
        }

        private Customer GetCustomer() => new Customer(_keyGenerator.New());
        
        [Theory]
        [InlineData(null, true)]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void Can_repeat_should_be_set_by_the_constructor_parameter(bool? ctorValue, bool expectedResult)
        {
            var instance = !ctorValue.HasValue ? new ArrayList<Customer>() : new ArrayList<Customer>(ctorValue.Value);
            instance.CanRepeat.Should().Be(expectedResult);
        }

        [Fact]
        public void When_cant_repeat_should_throw_exception_at_add_repeated_elemenet()
        {
            var instance = new ArrayList<Customer>(false);
            var el = GetCustomer();
            instance.Add(el);

            Action addAction = () => instance.Add(el);

            addAction.Should().ThrowExactly<ElementAlreadyExistsException>();
            instance.Should().HaveCount(1);
        }
        
        [Fact]
        public void When_cant_repeat_should_throw_exception_on_create_arraylist_from_array_with_repeted_items()
        {
            var el = GetCustomer();
            var array = new Customer[] {el, GetCustomer(), GetCustomer(), el, GetCustomer()};
            IEnumerable<Customer> asEnum = array.AsEnumerable();

            Action c1 = () => new ArrayList<Customer>(array, false);
            Action c2 = () => new ArrayList<Customer>(false, el, GetCustomer(), GetCustomer(), GetCustomer(), el, GetCustomer());
            Action c3 = () => new ArrayList<Customer>(asEnum, false);

            c1.Should().ThrowExactly<ElementAlreadyExistsException>();
            c2.Should().ThrowExactly<ElementAlreadyExistsException>();
            c3.Should().ThrowExactly<ElementAlreadyExistsException>();
        }


        [Fact]
        public void When_instance_is_generated_with_an_array_structure_should_have_the_same_elements()
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var instance = new ArrayList<Customer>(customersArray);

            instance.Count.Should().Be(3);
            instance.Should()
                .HaveSameCount(customersArray)
                .And.Equal(customersArray);
        }
        
        [Fact]
        public void When_instance_is_generated_with_a_enumerable_structure_should_have_the_same_elements()
        {
            IEnumerable<Customer> customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var instance = new ArrayList<Customer>(customersArray);

            instance.Count.Should().Be(3);
            instance.Should()
                .HaveSameCount(customersArray)
                .And.BeEquivalentTo(customersArray);
        }
        
        [Fact]
        public void Should_be_possible_to_interate_as_enumerable()
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var instance = new ArrayList<Customer>(customersArray);

            var currentPosition = 0;
            foreach (var element in instance)
            {
                element.Should().Be(customersArray[currentPosition]);
                currentPosition++;
            }
        }

        [Fact]
        public void Should_be_possible_acess_elements_between_indexes()
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var instance = new ArrayList<Customer>(customersArray);
            
            instance[0].Should().Be(customersArray[0]);
            instance[1].Should().Be(customersArray[1]);
            instance[2].Should().Be(customersArray[2]);
        }
        
        [Fact]
        public void Should_be_possible_updates_elements_between_indexes()
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var newEl = GetCustomer();
            var instance = new ArrayList<Customer>(customersArray);

            instance[1] = newEl;

            instance.Should().HaveCount(3).And.ContainInOrder(customersArray[0], newEl, customersArray[2]);
        }


        [Fact]
        public void When_acess_an_invalid_position_on_arraylist_should_throw_exception()
        {
            var instance = new ArrayList<Customer>();
            Action action = () =>
            {
                var b = instance[0];
            };
            action.Should().ThrowExactly<IndexOutOfRangeException>();
        }

        [Fact]
        public void Elements_should_keep_the_same_order_as_they_are_added()
        {
            var instance = new ArrayList<Customer>();
            var c1 = GetCustomer();
            var c2 = GetCustomer();
            var c3 = GetCustomer();

            instance.Add(c2);
            instance.Add(c3);
            instance.Add(c1);

            instance.GetAt(0).Should().Be(c2);
            instance[0].Should().Be(c2);
            instance.GetAt(1).Should().Be(c3);
            instance[1].Should().Be(c3);
            instance.GetAt(2).Should().Be(c1);
            instance[2].Should().Be(c1);
        }

        [Fact]
        public void Arraylist_can_be_clearable()
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var instance = new ArrayList<Customer>(customersArray);

            instance.Clear();

            instance.Should().HaveCount(0).And.BeEmpty();
            Action acessInvalidPostion = () => instance.GetAt(0);
            acessInvalidPostion.Should().ThrowExactly<IndexOutOfRangeException>();
        }

        [Fact]
        public void Constains_should_use_equals_for_comparer()
        {
            var c1 = GetCustomer();
            var c2 = GetCustomer();
            var instance = new ArrayList<Customer>();
            
            instance.Add(c2);

            instance.Contains(c2).Should().BeTrue();
            instance.Contains(c1).Should().BeFalse();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(0)]
        public void Should_copy_items_for_argument_array(int arrayIndex)
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var instance = new ArrayList<Customer>(customersArray);
            var copyLength = customersArray.Length - arrayIndex;
            var expectedArray = new Customer[copyLength];
            var currentExpectedPosition = 0;
            for (var i = arrayIndex; i < customersArray.Length; i++)
            {
                expectedArray[currentExpectedPosition] = customersArray[i];
                currentExpectedPosition++;
            }
            var copyArray = new Customer[copyLength];
            
            instance.CopyTo(copyArray, arrayIndex);

            copyArray.Should().Equal(expectedArray);
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(3)]
        public void When_try_to_copy_array_passing_an_invalid_position_should_thorw_exception(int position)
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var instance = new ArrayList<Customer>(customersArray);
            var copyArray = new Customer[10];
            
            Action copyAction = () => instance.CopyTo(copyArray, position);

            copyAction.Should().ThrowExactly<IndexOutOfRangeException>();
        }

        [Fact]
        public void When_try_to_copy_array_with_null_array_should_throw_exception()
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var instance = new ArrayList<Customer>(customersArray);
            
            Action copyAction = () => instance.CopyTo(null, 1);

            copyAction.Should().ThrowExactly<ArgumentException>().WithMessage("The array argument should be not null and have the length to support the transfer");
        }
        
        [Fact]
        public void When_try_to_copy_array_with_an_array_argument_that_not_support_the_elements_quantity_should_throw_exception()
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var instance = new ArrayList<Customer>(customersArray);
            var copyArray = new Customer[2];
            
            Action copyAction = () => instance.CopyTo(copyArray, 0);

            copyAction.Should().ThrowExactly<ArgumentException>().WithMessage("The array argument should be not null and have the length to support the transfer");
        }

        [Fact]
        public void Should_remove_element_when_find_element()
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var elementToDelete = customersArray[1];
            var instance = new ArrayList<Customer>(customersArray);

            instance.Remove(elementToDelete).Should().BeTrue();
            instance.Should().HaveCount(2).And.ContainInOrder(customersArray[0], customersArray[2]);
        }
        
        [Fact]
        public void Should_return_false_when_try_to_remove_an_inexistent_element()
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var anotherElement = GetCustomer();
            var instance = new ArrayList<Customer>(customersArray);

            instance.Remove(anotherElement).Should().BeFalse();
        }

        [Fact]
        public void Should_be_possible_to_count_elements()
        {
            var customersArray = new Customer[] { GetCustomer(), GetCustomer(), GetCustomer() };
            var instance = new ArrayList<Customer>(customersArray);

            instance.Count.Should().Be(3);
        }

        [Fact]
        public void The_structure_not_is_read_only()
        {
            var instance = new ArrayList<Customer>();

            instance.IsReadOnly.Should().BeFalse();
        }

        [Fact]
        public void Should_be_possible_to_add_how_many_elements_the_memory_supports()
        {
            var instance = new ArrayList<Customer>();
            const int qtdElements = 2000000;

            for (int i = 0; i < qtdElements; i++)
                instance.Add(GetCustomer());

            instance.Count.Should().Be(qtdElements);
        }

        [Fact]
        public void Should_be_possible_to_get_the_first_element()
        {
            var instance = new ArrayList<Customer>();

            var first = GetCustomer();
            instance.Add(first);
            instance.Add(GetCustomer());
            
            instance.First().Should().Be(first);
            instance.FirstOrDefault().Should().Be(first);
        }
        
        [Fact]
        public void When_try_to_get_first_from_an_empty_list_should_throw_exception()
        {
            var instance = new ArrayList<Customer>();

            Action action = () => instance.First();

            action.Should().ThrowExactly<IndexOutOfRangeException>();
        }
        
        [Fact]
        public void When_get_first_or_default_with_empty_list_should_return_default_from_type()
        {
            var instanceReferenceType = new ArrayList<Customer>();
            var instanceValueType = new ArrayList<int>();

            instanceReferenceType.FirstOrDefault().Should().BeNull();
            instanceValueType.FirstOrDefault().Should().Be(default);
        }
        
        [Fact]
        public void Should_be_possible_to_get_the_last_element()
        {
            var instance = new ArrayList<Customer>();

            var last = GetCustomer();
            instance.Add(GetCustomer());
            instance.Add(last);
            
            instance.Last().Should().Be(last);
        }
        
        [Fact]
        public void When_try_to_get_last_from_an_empty_list_should_throw_exception()
        {
            var instance = new ArrayList<Customer>();

            Action action = () => instance.Last();

            action.Should().ThrowExactly<IndexOutOfRangeException>();
        }
        
        [Fact]
        public void When_get_last_or_default_with_empty_list_should_return_default_from_type()
        {
            var instanceReferenceType = new ArrayList<Customer>();
            var instanceValueType = new ArrayList<int>();

            instanceReferenceType.LastOrDefault().Should().BeNull();
            instanceValueType.LastOrDefault().Should().Be(default);
        }


        [Fact]
        public void Should_be_possible_to_change_an_element_at_position()
        {
            var oldEl = GetCustomer();
            var newEl = GetCustomer();
            var instance = new ArrayList<Customer>();
            instance.Add(oldEl);
            
            instance.ChangeAt(newEl, 0);

            instance.Should().HaveCount(1).And.OnlyContain(x => Equals(x, newEl));
        }
        
        [Fact]
        public void When_try_to_change_an_element_at_an_invalid_position_should_throw_exception()
        {
            var element = GetCustomer();
            var instance = new ArrayList<Customer>();
            
            Action changeAction = () => instance.ChangeAt(element, 0);

            changeAction.Should().ThrowExactly<IndexOutOfRangeException>();
        }

        [Fact]
        public void Should_be_possible_to_push_element_at_any_position()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            var instance = new ArrayList<Customer>();
            instance.Add(first);
            instance.Add(third);
            
            instance.PushAt(second, 1);

            instance.Should().HaveCount(3).And.ContainInOrder(first, second, third);
        }
        
        [Fact]
        public void When_try_to_push_an_element_at_an_invalid_position_should_throw_exception()
        {
            var element = GetCustomer();
            var instance = new ArrayList<Customer>();
            
            Action changeAction = () => instance.PushAt(element, 0);

            changeAction.Should().ThrowExactly<IndexOutOfRangeException>();
        }
        
        [Fact]
        public void Should_be_possible_to_remove_an_element_at_any_position()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            var instance = new ArrayList<Customer>();
            instance.Add(first);
            instance.Add(second);
            instance.Add(third);

            instance.RemoveAt(1);

            instance.Should().HaveCount(2).And.ContainInOrder(first, third);
        }
        
        [Fact]
        public void When_try_to_remove_an_element_at_an_invalid_position_should_throw_exception()
        {
            var element = GetCustomer();
            var instance = new ArrayList<Customer>();
            
            Action changeAction = () => instance.RemoveAt(0);

            changeAction.Should().ThrowExactly<IndexOutOfRangeException>();
        }

        [Fact]
        public void Should_be_possible_to_add_an_element_at_first_position()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            var instance = new ArrayList<Customer>();
            instance.Add(second);
            instance.Add(third);

            instance.AddFirst(first);

            instance.Should().HaveCount(3).And.ContainInOrder(first, second, third);
        }

        [Fact]
        public void Should_be_possible_to_add_an_enumerable_from_elements()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            IEnumerable<Customer> array = new[] {first, second, third};
            var instance = new ArrayList<Customer>();
            
            instance.AddRange(array);

            instance.Should().HaveCount(3).And.ContainInOrder(first, second, third);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        public void Should_be_possible_to_reverse_array_positions(int quantityOfElements)
        {
            var array = new Customer[quantityOfElements];
            for (int i = 0; i < quantityOfElements; i++)
                array[i] = GetCustomer();
            
            var instance = new ArrayList<Customer>(array);
            
            instance.Reverse();

            instance.Should().HaveCount(quantityOfElements).And.ContainInOrder(array.Reverse());
        }

        [Fact]
        public void Should_be_possible_to_find_an_element_index()
        {
            var includEl = GetCustomer();
            var excludEl = GetCustomer();
            var instance = new ArrayList<Customer>(true, includEl);

            instance.IndexOf(excludEl).Should().Be(-1);
            instance.IndexOf(includEl).Should().Be(0);
        }
        
        [Fact]
        public void Should_be_possible_to_check_if_the_list_is_empty()
        {
            var instance = new ArrayList<Customer>();

            instance.IsEmpty.Should().BeTrue();
            
            instance.Add(GetCustomer());

            instance.IsEmpty.Should().BeFalse();
            
            instance.Clear();

            instance.IsEmpty.Should().BeTrue();
        }

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(4, -1)]
        [InlineData(3, -1)]
        [InlineData(0, -1)]
        [InlineData(2, -1)]
        [InlineData(0, 1)]
        [InlineData(0, -2)]
        [InlineData(0, 5)]
        [InlineData(2, 2)]
        public void Should_be_possible_to_slice_list(int startPosition, int length)
        {
            const int arrayLength = 4;
            var elements = new Customer[]{GetCustomer(), GetCustomer(), GetCustomer(),GetCustomer()};
            var instance = new ArrayList<Customer>(elements);

            if (startPosition < 0 || startPosition > 3 || length < -1 || (startPosition + length) > arrayLength)
            {
                Action sliceAction = () => instance.Slice(startPosition, length);
                sliceAction.Should().ThrowExactly<IndexOutOfRangeException>();
                return;
            }

            var finalPos = length == -1 ? arrayLength : startPosition + length;
            var countExpected = length == -1 ? arrayLength - startPosition : length;
            var arrayExpected = elements[startPosition..finalPos];

            instance.Slice(startPosition, length)
                .Should().HaveCount(countExpected).And.Equal(arrayExpected);
        }
        
        [Fact]
        public void Should_return_the_first_element_that_is_true_for_an_predicate()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            const string commumName = "Adriano";
            first.Name = commumName;
            second.Name = "Renata";
            third.Name = commumName;
            var instance = new ArrayList<Customer>(true, first, second, third);

            instance.SearchFirst(x => x.Name.Contains(commumName)).Should().Be((0, first));
        }
        
        [Fact]
        public void Should_return_the_last_element_that_is_true_for_an_predicate()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            const string commumName = "Adriano";
            first.Name = commumName;
            second.Name = "Renata";
            third.Name = commumName;
            var instance = new ArrayList<Customer>(true, first, second, third);

            instance.SearchLast(x => x.Name.Contains(commumName)).Should().Be((2, third));
        }

        [Fact]
        public void Should_return_all_elements_that_is_true_for_an_predicate()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            const string commumName = "Adriano";
            first.Name = commumName;
            second.Name = "Renata";
            third.Name = commumName;
            var instance = new ArrayList<Customer>(true, first, second, third);

            instance.SearchAll(x => x.Name.Contains(commumName))
                .Should()
                .HaveCount(2)
                .And.OnlyHaveUniqueItems(x => x.Key)
                .And.OnlyContain(x => x.Name == commumName);
        }

        [Fact]
        public void Should_be_possible_to_sort_with_bubble_sort_algorithm()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            var forth = GetCustomer();
            var fifth = GetCustomer();
            
            var instance = new ArrayList<Customer>(true, third, fifth, first, second, forth, first, second);
            
            instance.BubbleSort((leftEl, rightEl) => leftEl.Key - rightEl.Key);

            instance.Should().HaveCount(7).And.ContainInOrder(first, first, second, second, third, forth, fifth);
        }
        
        [Fact]
        public void When_the_list_already_ordened_bubble_sort_cant_change_anything()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            var forth = GetCustomer();
            var fifth = GetCustomer();
            
            var instance = new ArrayList<Customer>(true, first, first, second, second, third, forth, fifth);
            
            instance.BubbleSort((leftEl, rightEl) => leftEl.Key - rightEl.Key);

            instance.Should().HaveCount(7).And.ContainInOrder(first, first, second, second, third, forth, fifth);
        }

        [Fact]
        public void Should_be_possible_to_sort_with_insertion_sort_algorithm()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            var forth = GetCustomer();
            var fifth = GetCustomer();
            
            var instance = new ArrayList<Customer>(true, third, fifth, first, second, forth, first, second);
            
            instance.InsertionSort((leftEl, rightEl) => leftEl.Key - rightEl.Key);

            instance.Should().HaveCount(7).And.ContainInOrder(first, first, second, second, third, forth, first);
        }

        [Fact]
        public void Should_be_possible_to_sort_with_selection_sort_algorithm()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            var forth = GetCustomer();
            var fifth = GetCustomer();
            
            var instance = new ArrayList<Customer>(true, third, fifth, first, second, forth, first, second);
            
            instance.SelectionSort((leftEl, rightEl) => leftEl.Key - rightEl.Key);

            instance.Should().HaveCount(7).And.ContainInOrder(first, first, second, second, third, forth, first);
        }

        [Fact]
        public void Should_be_possible_to_sort_with_merge_sort_algorithm()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            var forth = GetCustomer();
            var fifth = GetCustomer();
            
            var instance = new ArrayList<Customer>(true, third, fifth, first, second, forth, first, second);
            
            instance.MergeSort((leftEl, rightEl) => leftEl.Key - rightEl.Key);

            instance.Should().HaveCount(7).And.ContainInOrder(first, first, second, second, third, forth, first);
        }

        [Fact]
        public void Should_be_possible_to_sort_with_quick_sort_algorithm()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            var forth = GetCustomer();
            var fifth = GetCustomer();
            
            var instance = new ArrayList<Customer>(true, third, fifth, first, second, forth, first, second);
            
            instance.QuickSort((leftEl, rightEl) => leftEl.Key - rightEl.Key);

            instance.Should().HaveCount(7).And.ContainInOrder(first, first, second, second, third, forth, first);
        }

        [Fact]
        public void Should_be_possible_to_sort_with_heap_sort_algorithm()
        {
            var first = GetCustomer();
            var second = GetCustomer();
            var third = GetCustomer();
            var forth = GetCustomer();
            var fifth = GetCustomer();
            
            var instance = new ArrayList<Customer>(true, third, fifth, first, second, forth, first, second);
            
            instance.HeapSort((leftEl, rightEl) => leftEl.Key - rightEl.Key);

            instance.Should().HaveCount(7).And.ContainInOrder(first, first, second, second, third, forth, first);
        }
    }
}